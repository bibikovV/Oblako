using System;
using System.IO;
using System.Linq;
using System.Net;


namespace NetFileManagerServer
{
    
  
    

    public class FileItem
    {
        public FileItem(string path)
        {
            this.Path = path;

            if (this.IsLocal)
            { // локальный файл
                if (!FileItem.CheckLocalFileExists(path))
                    throw new FileNotFoundException("Локальный файл по указанному пути не найден: {0}", path);

                this.Name = path.Substring(path.LastIndexOf('\\') + 1);
            }
            else
            { // веб-файл
                if (!FileItem.CheckWebFileExists(path))
                    throw new FileNotFoundException("Веб-файл по указанному пути не найден: {0}", path);

                this.Name = path.Substring(path.LastIndexOf('/') + 1);
                this.Name = this.Name.Substring(0, this.Name.LastIndexOf('.'));
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Флаг, указывающий что файл находится на локальном компьютере
        /// </summary>
        public bool IsLocal { get { return !this.Path.StartsWith("http://"); } }

        /// <summary>
        /// Проверяет наличие WEB-файла
        /// </summary>
        /// <param name="path">Путь</param>
        public static bool CheckWebFileExists(string path)
        {
            bool success = true;

            HttpWebResponse response = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Method = "HEAD";

            try { response = (HttpWebResponse)request.GetResponse(); }
            catch (WebException) { success = false; }
            finally
            {
                if (response != null)
                    response.Close();
            }

            return success;
        }

        /// <summary>
        /// Проверяет наличие локального файла
        /// </summary>
        /// <param name="path">Путь</param>
        public static bool CheckLocalFileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
