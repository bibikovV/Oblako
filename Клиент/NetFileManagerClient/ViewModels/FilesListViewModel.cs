using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Xml.Linq;
using System.Windows.Data;


namespace NetFileManagerClient.ViewModels
{
    
   
    

    public class FilesListViewModel : INotifyPropertyChanged
    {
       
        private List<string> files;
        private string selectedFile;
        private bool isServerReady;
        

        
        /// <summary>
        /// Конструктор
        /// </summary>
        public FilesListViewModel()
        {
            this.files = new List<string>();
            this.Files = CollectionViewSource.GetDefaultView(this.files);

            this.OpenCommand = new Command(this.OpenFile);
            this.OpenFolderCommand = new Command((args) => { System.Diagnostics.Process.Start(App.FILESPATH); });
            this.RefreshCommand = new Command(this.RefreshList);
            this.SendFileCommand = new Command(this.SendFile);

            NetService.Instance.OnMessageReceived += NetService_OnMessageReceived;
            NetService.Instance.OnFileReceived += Instance_OnFileReceived;
            NetService.Instance.OnStatusChanged += Instance_OnStatusChanged;

            this.ServerIp = "127.0.0.1";
        }
       

        
        /// <summary>
        /// Загрузить и открыть файл
        /// </summary>
        private void OpenFile(object o)
        {
            if(!string.IsNullOrEmpty(this.selectedFile))
                NetService.Instance.RequestFile(this.selectedFile);
        }



        /// <summary>
        /// Отправить файл
        /// </summary>
        private void SendFile(object o)
        {
            
                NetService.Instance.RequestFile(this.selectedFile);
        }

        /// <summary>
        /// Обновить список
        /// </summary>
        private void RefreshList(object o)
        {
            this.files.Clear();

            NetService.Instance.RequestList();
        }
       

        
        /// <summary>
        /// Файлы
        /// </summary>
        public ICollectionView Files { get; private set; }

        /// <summary>
        /// Выбранный файл
        /// </summary>
        public string SelectedFile
        {
            get { return this.selectedFile; }
            set
            {
                if (this.selectedFile != value)
                {
                    this.selectedFile = value;

                    this.RaisePropertyChanged(() => this.SelectedFile);
                }
            }
        }

        /// <summary>
        /// IP-адрес сервера
        /// </summary>
        public string ServerIp
        {
            get { return NetService.Instance.ServerIp.ToString(); }
            set
            {
                IPAddress ipAddr = null;

                if (IPAddress.TryParse(value.Trim(), out ipAddr))
                    NetService.Instance.SetServerIp(ipAddr);

                this.RaisePropertyChanged(() => this.ServerIp);
            }
        }

        /// <summary>
        /// Доступность сервера
        /// </summary>
        public bool IsServerReady
        {
            get { return this.isServerReady; }
            set
            {
                if (this.isServerReady != value)
                {
                    this.isServerReady = value;

                    this.RaisePropertyChanged(() => this.IsServerReady);
                }
            }
        }
        

        
        public Command OpenCommand { get; private set; }
        public Command OpenFolderCommand { get; private set; }
        public Command RefreshCommand { get; private set; }
        public Command SendFileCommand { get; private set; }



        private void NetService_OnMessageReceived(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            if (message[0] == '<' && message[message.Length - 1] == '>')
            { // скорее всего XML
                XDocument xDoc = XDocument.Parse(message);

                if (xDoc.Root.Name != "Files")
                    return;

                this.files.Clear();

                foreach (XElement xEle in xDoc.Root.Elements("File"))
                    this.files.Add(xEle.Value);

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => {
                    this.Files.Refresh();
                }));
            }
            else
            { // сообщение
                System.Diagnostics.Debug.WriteLine(string.Format("{0:hh:mm:ss} FilesListViewModel.NetService_OnMessageReceived: {1}", DateTime.Now, message));
            }
        }

        private void Instance_OnFileReceived(string filePath)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                // чутка погодём, а то некоторые файлы не успевают записаться и выпендриваются
                System.Threading.Thread.Sleep(500);

                System.Diagnostics.Process.Start(filePath);
            });
        }

        private void Instance_OnStatusChanged(NetService.StatusChangedEventArgs e)
        {
            if (e.Status == NetService.Status.Connected)
                this.IsServerReady = true;
            else
                this.IsServerReady = false;
        }
        

        
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyExpression.Name));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
