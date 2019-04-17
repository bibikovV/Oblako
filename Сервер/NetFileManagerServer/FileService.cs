using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace NetFileManagerServer
{
    
    
  

    public class FileService
    {
       
        private static FileService instance;
        private static object syncRoot = new object();
        private List<FileItem> files;
   
        
        private FileService()
        {
            this.files = new List<FileItem>();

            this.LoadConfig();
        }
     

        
        /// <summary>
        /// Загрузить конфиг
        /// </summary>
        private void LoadConfig()
        {
            XDocument xDoc;

            if (!File.Exists("config.xml"))
                xDoc = new XDocument(new XElement("Config", new XElement("Files")));
            else
            {
                xDoc = XDocument.Load("config.xml");

                if (xDoc.Root.Element("Files") == null)
                    xDoc.Root.Add(new XElement("Files"));
                else
                    foreach (XElement xEle in xDoc.Root.Element("Files").Elements("File"))
                        this.AddFile(xEle.Value);
            }
        }

        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        private void SaveConfig()
        {
            XDocument xDoc;

            if (!File.Exists("config.xml"))
                xDoc = new XDocument(new XElement("Config", new XElement("Files")));
            else
            {
                xDoc = XDocument.Load("config.xml");

                if (xDoc.Root.Element("Files") == null)
                    xDoc.Root.Add(new XElement("Files"));
            }

            XElement filesEle = xDoc.Root.Element("Files");
            filesEle.RemoveNodes();

            foreach (FileItem fi in this.files)
                filesEle.Add(new XElement("File", fi.Path));

            xDoc.Save("config.xml");
        }
       

        
        public static FileService Instance
        {
            get
            {
                if (FileService.instance == null)
                    lock (FileService.syncRoot)
                        if (FileService.instance == null)
                            FileService.instance = new FileService();

                return FileService.instance;
            }
        }

        /// <summary>
        /// Коллекция файлов
        /// </summary>
        public List<FileItem> Files
        {
            get { return this.files; }
            private set { this.files = value; }
        }
       

        
        /// <summary>
        /// Добавить файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void AddFile(string path)
        {
            if (File.Exists(path) && this.files.FirstOrDefault(x => x.Path == path) == null)
            {
                this.files.Add(new FileItem(path));

                this.SaveConfig();
            }
        }

        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="name">Имя файла</param>
        public void RemoveFile(string name)
        {
            FileItem fi = this.files.FirstOrDefault(x => x.Name == name);

            if (fi != null)
            {
                this.files.Remove(fi);

                this.SaveConfig();
            }
        }

        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="file">Файл</param>
        public void RemoveFile(FileItem file)
        {
            if (this.files.Contains(file))
            {
                this.files.Remove(file);

                this.SaveConfig();
            }
        }

        /// <summary>
        /// Возвращает список файлов в виде XML
        /// </summary>
        public string GetFilesList()
        {
            XDocument xDoc = new XDocument(new XElement("Files"));

            foreach (FileItem file in FileService.instance.Files)
                xDoc.Root.Add(new XElement("File", file.Name));

            return xDoc.ToString();
        }
        
    }
}
