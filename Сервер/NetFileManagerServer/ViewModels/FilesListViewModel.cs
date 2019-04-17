namespace NetFileManagerServer.ViewModels
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Xml.Linq;
    using System.Windows.Data;
    using Forms = System.Windows.Forms;
    #endregion

    public class FilesListViewModel : INotifyPropertyChanged
    {
        #region Свойства
        private FileItem selectedFile;
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        public FilesListViewModel()
        {
            this.Files = CollectionViewSource.GetDefaultView(FileService.Instance.Files);

            this.AddCommand = new Command(this.AddFile);
            this.RemoveCommand = new Command(this.RemoveFile);
        }
        #endregion

        #region Методы
        /// <summary>
        /// Добавить локальный файл
        /// </summary>
        private void AddFile(object o)
        {
            Forms.OpenFileDialog selectFilesDialog = new Forms.OpenFileDialog();
            selectFilesDialog.InitialDirectory = "D:\\";
            selectFilesDialog.CheckFileExists = true;
            selectFilesDialog.CheckPathExists = true;
            selectFilesDialog.Multiselect = true;
            selectFilesDialog.Filter = "Все файлы (*.*)|*.*";
            selectFilesDialog.DefaultExt = "xml";
            selectFilesDialog.RestoreDirectory = true;

            if (selectFilesDialog.ShowDialog() == Forms.DialogResult.OK)
            {
                foreach (string path in selectFilesDialog.FileNames)
                    FileService.Instance.AddFile(path);
                
                this.Files.Refresh();

                NetService.Instance.SendMessage(FileService.Instance.GetFilesList());
            }
        }

        /// <summary>
        /// Удалить файл
        /// </summary>
        private void RemoveFile(object o)
        {
            if (this.selectedFile == null)
                return;

            FileService.Instance.RemoveFile(this.selectedFile);

            this.Files.Refresh();

            NetService.Instance.SendMessage(FileService.Instance.GetFilesList());
        }
        #endregion

        #region Общие свойства
        /// <summary>
        /// Файлы
        /// </summary>
        public ICollectionView Files { get; private set; }

        /// <summary>
        /// Выбранный файл
        /// </summary>
        public FileItem SelectedFile
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
        #endregion

        #region Команды
        public Command AddCommand { get; private set; }
        public Command RemoveCommand { get; private set; }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyExpression.Name));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
