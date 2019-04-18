using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace NetFileManagerClient
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public static string User;

        public Auth()
        {
            InitializeComponent();
            TextBoxLogin.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string file = TextBoxLogin.Text + ".txt ";
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Auth\\" + file))
            {
                string check = "";
                string logpas = TextBoxLogin.Text + ";" + TextBoxPassword.Text + ";";
                
                
                check = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Auth\\" + file);
                

                    if (logpas == check)
                    {
                    User =  TextBoxLogin.Text;
                        MainWindow wn = new MainWindow();
                        wn.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Данные введены неверно!");
                    }
            }
            else
            {
                this.Hide();
                
                File.Create(Directory.GetCurrentDirectory() + "\\Auth\\"+TextBoxLogin.Text+".txt").Close();
                StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\Auth\\" + TextBoxLogin.Text + ".txt");
                writer.Write(this.TextBoxLogin.Text + ";");
                writer.Write(this.TextBoxPassword.Text + ";");
                writer.Close();

                MessageBox.Show("Ваши данные сохранены!");
                Auth au = new Auth();
                au.Show();
                
            }
        }
    }
}
