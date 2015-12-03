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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace PC_store
{
    /// <summary>
    /// Логика взаимодействия для Config.xaml
    /// </summary>
    public partial class Config : Window
    {
        public Config()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
        }

        private void ConectionToServer_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
                MainWindow.conection.ConectionToServer(Server.Text, DataBase.Text, Login.Text,passmord.Password);
            }
            catch(SqlException)
            {
                MessageBox.Show("Ошибка подключения к серверу!", "Ошибка!");
                flag = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
                flag = false;
            }
            if (flag) this.Close();
        }
    }
}
