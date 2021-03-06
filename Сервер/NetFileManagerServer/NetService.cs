﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace NetFileManagerServer
{

    
   

    /// <summary>
    /// Сервис для работы с сетью
    /// </summary>
    public class NetService
    {
       
        private static int SERVERUDPPORT = 10024;
        private static int SERVERTCPPORT = 10025;
        private static string FILESLISTCOMMAND = "list";
        private static string PUSHCOMMAND = "push";

        private static NetService instance;
        private static object syncRoot = new object();

        private Dictionary<IPAddress, int> udpClients; // клиенты текстовых сообщений
        private CancellationTokenSource cancelTokenSource;
        private TcpListener tcpListener;
        

        
        private NetService()
        {
            this.udpClients = new Dictionary<IPAddress, int>();

            this.StartListen();
        }
        

       
        /// <summary>
        /// Начать прослушку интерфесов
        /// </summary>
        private void StartListen()
        {
            this.cancelTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() => this.ListenUDP(this.cancelTokenSource.Token), this.cancelTokenSource.Token);

            this.tcpListener = new TcpListener(IPAddress.Any, NetService.SERVERTCPPORT);
            this.tcpListener.Start();

            for (int i = 0; i < 5; i++) // принимаем до 5 входящих подключений одновременно
                Task.Factory.StartNew(() => this.ListenTCP(this.cancelTokenSource.Token), this.cancelTokenSource.Token);
        }

        /// <summary>
        /// Прослушка интерфейса по протоколу UDP
        /// </summary>
        private void ListenUDP(CancellationToken cancelToken)
        {
            IPEndPoint ipEP;
            UdpClient client = null;

            try
            {
                ipEP = new IPEndPoint(IPAddress.Any, NetService.SERVERUDPPORT);
                client = new UdpClient(ipEP);
                
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    
                    IPEndPoint ipep = null;
                    byte[] messageBytes = client.Receive(ref ipep);

                    IPAddress clientIp = ipep.Address;
                    int clientPort = ipep.Port;

                    // запомним ип и порт с которого пришел запрос, понадобится для отправки многоадресных сообщений
                    if (!this.udpClients.ContainsKey(clientIp))
                        this.udpClients.Add(clientIp, clientPort);
                    else
                        this.udpClients[clientIp] = clientPort;

                    System.Diagnostics.Debug.WriteLine("{0:hh:mm:ss} NetService.ListenUDP => message from {1}:{2}", DateTime.Now, clientIp, clientPort);

                    string message = Encoding.UTF8.GetString(messageBytes);

                    if (message == NetService.PUSHCOMMAND) // поддержка связи
                        continue;
                    if (message == NetService.FILESLISTCOMMAND) // запрос списка файлов
                        Task.Factory.StartNew(() => NetService.Instance.SendMessage(clientIp, clientPort, FileService.Instance.GetFilesList()));
                }
            }
            catch (OperationCanceledException) { }
            catch (SocketException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0:hh:mm:ss} NetService.ListenUDP => Exception: {1}", DateTime.Now, ex.Message));

                Environment.Exit(1);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        /// <summary>
        /// Прослушка интерфейса по протоколу TCP
        /// </summary>
        private void ListenTCP(CancellationToken cancelToken)
        {
            TcpClient client = null;

            try
            {
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();

                    client = this.tcpListener.AcceptTcpClient(); // приём запроса

                    NetworkStream s = client.GetStream();

                    // получаем имя запрошенного файла
                    byte[] buffer = new byte[256];
                    int fileNameLength = s.Read(buffer, 0, buffer.Length);
                    string fileName = System.Text.Encoding.UTF8.GetString(buffer, 0, fileNameLength);
                    string filePath = string.Empty;

                    if (string.IsNullOrWhiteSpace(fileName))
                        continue;

                    // проверяем, есть ли в списке доступных файлов запрошенный
                    FileItem fi = FileService.Instance.Files.FirstOrDefault(x => x.Name == fileName);

                    if (fi != null)
                        filePath = fi.Path;

                    if (!string.IsNullOrEmpty(filePath))
                    { // файл есть, отдаём
                        using (var fileIO = File.OpenRead(filePath))
                        {
                            //s.Write(BitConverter.GetBytes(fileIO.Length), 0, 8);
                            s.Write(BitConverter.GetBytes(fileIO.Length), 0, fileIO.Length.ToString().Length);

                            buffer = new byte[131072 * 8];
                            int count;

                            while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                                s.Write(buffer, 0, count);
                        }
                    }

                    s.Close();
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0:hh:mm:ss} NetService.ListenTCP => Exception: {1}", DateTime.Now, ex.Message));

                Environment.Exit(1);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }
        

        
        /// <summary>
        /// NetService Instance
        /// </summary>
        public static NetService Instance
        {
            get
            {
                if (NetService.instance == null)
                    lock (NetService.syncRoot)
                        if (NetService.instance == null)
                            NetService.instance = new NetService();

                return NetService.instance;
            }
        }
       
        
        /// <summary>
        /// Отправить сообщение всем известным адресатам по протоколу UDP
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void SendMessage(string message)
        {
            foreach (KeyValuePair<IPAddress, int> client in this.udpClients)
                this.SendMessage(client.Key, client.Value, message);
        }

        /// <summary>
        /// Отправить сообщение по протоколу UDP
        /// </summary>
        /// <param name="clientIp">IP-адрес получателя</param>
        /// <param name="clientPort">Порт получателя</param>
        /// <param name="message">Сообщение</param>
        public void SendMessage(IPAddress clientIp, int clientPort, string message)
        {
            UdpClient client = new UdpClient();
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            IPEndPoint ipep = new IPEndPoint(clientIp, clientPort);

            client.Send(messageBytes, messageBytes.Length, ipep);
        }

        /// <summary>
        /// Остановить все процессы прослушки интерфейсов
        /// </summary>
        public void Cancel()
        {
            if (this.cancelTokenSource != null)
                this.cancelTokenSource.Cancel();
        }
       
    }
}
