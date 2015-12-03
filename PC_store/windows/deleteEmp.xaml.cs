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
    /// Логика взаимодействия для deleteEmp.xaml
    /// </summary>
    public partial class deleteEmp : Window
    {
        public deleteEmp()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
                flag=MainWindow.conection.DelEmp(Convert.ToInt32(textBox.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка!");
                flag = false;
            }
            if (flag) this.Close();
        }

    }
}
