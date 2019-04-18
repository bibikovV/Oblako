
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetFileManagerClient.Views
{
    public partial class FilesListView : UserControl
    {
        
        public FilesListView()
        {
            
            InitializeComponent();
            

            
           

        }

        private void LabelUser_Loaded(object sender, RoutedEventArgs e)
        {
            LabelUser.Content = Auth.User;

        }
    }
}
