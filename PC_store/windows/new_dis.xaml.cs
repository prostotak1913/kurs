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
    /// Логика взаимодействия для new_dis.xaml
    /// </summary>
    public partial class new_dis : Window
    {
        public new_dis()
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
                flag = MainWindow.conection.AddDiscount(name.Text, Convert.ToInt16(dis.Text));
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
