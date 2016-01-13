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

namespace PC_store
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        
        public Login()
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
                flag = MainWindow.conection.authorization(Login_log.Text, passwordBox.Password);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Ошибка при вводе пароля или логина","Ошибка логина или пароля!");
                flag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),ex.Message);
                flag = false;
            }
            if (flag) this.Close();
        }
    }
}
