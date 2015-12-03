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
    /// Логика взаимодействия для new_product.xaml
    /// </summary>
    public partial class new_product : Window
    {
        public new_product()
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
                MainWindow.conection.new_product(name.Text, spec.Text, Convert.ToDouble(price.Text));
            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            if (flag) this.Close();
        }

    }
}

