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
using System.Data;

namespace PC_store
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket_Window : Window
    {
        List<int> li = new List<int>();
        public Basket_Window()
        {
            InitializeComponent();
            dataGrid.ItemsSource =  MainWindow.bsk.basket;
            double d=0;
            for(int i = 0;i< MainWindow.bsk.basket.Count;i++) d += MainWindow.bsk.basket[i].цена;
            all_price.Text = d.ToString();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
        }

        private void new_emp_Click(object sender, RoutedEventArgs e)
        {
            new_order no= new new_order();
            no.Show();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.bsk.del(Convert.ToInt32(id_prod.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            dataGrid.Items.Refresh();
        }

    }
}
