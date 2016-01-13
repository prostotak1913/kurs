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
    /// Логика взаимодействия для intake_to_str.xaml
    /// </summary>
    public partial class intake_to_str : Window
    {
        public intake_to_str()
        {
            InitializeComponent();
            prod.ItemsSource = MainWindow.conection.fill_Prod();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
        }

        private void intake_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
                flag = MainWindow.conection.intake(prod.SelectedIndex+1, Convert.ToInt32(amount.Text));
            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.ToString(), "Ошибка!");
            }
            if (flag) MessageBox.Show("Добавлено", "Ошибка!");
        }

        private void new_prod_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            new_product np = new new_product();
            np.Show();
            np.Closed += (object s, EventArgs ea) => { this.IsEnabled = true; } ;
        }

        //private void Np_Closed(object sender, EventArgs e)
        //{
        //    this.IsEnabled = true;
        //}
    }
}
