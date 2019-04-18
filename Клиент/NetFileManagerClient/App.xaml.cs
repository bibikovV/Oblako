using System;
using System.IO;
using System.Windows;

namespace NetFileManagerClient
{
    public partial class Application : System.Windows.Application
    {
        internal static string FILESPATH = "DownloadedFiles";

        public Application()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Application.FILESPATH);

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
            DirectoryInfo dir = new DirectoryInfo(Application.FILESPATH);

            foreach (FileInfo file in dir.GetFiles())
            {
                try { file.Delete(); }
                catch { }
            }

            NetService.Instance.Cancel();
        }
    }
}
