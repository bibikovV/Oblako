using System;
using System.IO;
using System.Windows;

namespace NetFileManagerClient
{
    public partial class App : Application
    {
        internal static string FILESPATH = "DownloadedFiles";

        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(App.FILESPATH);

            if (!dir.Exists)
                dir.Create();
            else
                foreach (FileInfo file in dir.GetFiles())
                {
                    try { file.Delete(); }
                    catch { }
                }
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(App.FILESPATH);

            foreach (FileInfo file in dir.GetFiles())
            {
                try { file.Delete(); }
                catch { }
            }

            NetService.Instance.Cancel();
        }
    }
}
