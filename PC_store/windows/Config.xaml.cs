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
using System.IO;

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
            if(MainWindow.configuration.SavingConnectionString)
            {
                save.IsChecked = MainWindow.configuration.SavingConnectionString;
                Server.Text = MainWindow.configuration.Server;
                DataBase.Text = MainWindow.configuration.DataBase;
                IntegrateSecurity.IsChecked = MainWindow.configuration.IntegratedSecurity;
                if (!MainWindow.configuration.IntegratedSecurity)
                {
                    Login.Text = MainWindow.configuration.User;
                    passmord.Password = MainWindow.configuration.Password;
                }
            }
        }

        private void ConectionToServer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.configuration = ((bool)IntegrateSecurity.IsChecked)? 
                new ConectionConfig((bool)IntegrateSecurity.IsChecked, Server.Text, DataBase.Text):
                new ConectionConfig((bool)IntegrateSecurity.IsChecked, Server.Text, DataBase.Text, Login.Text, passmord.Password);

            bool flag = true;
            try
            {
                if ((bool)IntegrateSecurity.IsChecked)
                    MainWindow.conection.ConectionToServer(Server.Text, DataBase.Text);
                else
                    MainWindow.conection.ConectionToServer(Server.Text, DataBase.Text, Login.Text, passmord.Password);
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
            if (flag)
            {
                if ((bool)save.IsChecked)
                {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    var file = new FileStream("server.set", FileMode.Create, FileAccess.Write);
                    formatter.Serialize(file, MainWindow.configuration);
                    file.Close();
                }
                else
                {
                    var temp = new ConectionConfig(true, "", "");
                    temp.SavingConnectionString = false;
                }
                this.Close();
            }
        }
    }
}
