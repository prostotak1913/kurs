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
    /// Логика взаимодействия для new_order.xaml
    /// </summary>
    public partial class new_order : Window
    {
        double d;
        double dv;

        public new_order()
        {
            InitializeComponent();
            d = 0;
            for (int i = 0; i < MainWindow.bsk.basket.Count; i++) d += MainWindow.bsk.basket[i].цена;
            price.Text = d.ToString();
            comboBox.ItemsSource = MainWindow.conection.fill_dis();
            dataGrid.ItemsSource = MainWindow.bsk.basket;
            dv = d;
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
        }

        private void create_order_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (id_client.Text == "№ клиента") id_client.Text ="";
            try
            {
                for (int i = 0; i < MainWindow.bsk.basket.Count; i++)
                {
                    try
                    {
                        if (comboBox.SelectedIndex == -1)
                            if (id_client.Text == "") flag = MainWindow.conection.new_Order(MainWindow.bsk.basket[i].код, MainWindow.bsk.basket[i].количество, dv);
                            else flag = MainWindow.conection.new_Order2(MainWindow.bsk.basket[i].код, MainWindow.bsk.basket[i].количество, Convert.ToInt32(id_client.Text), dv);
                        else
                            if (id_client.Text == "") flag = MainWindow.conection.new_Order1(MainWindow.bsk.basket[i].код, MainWindow.bsk.basket[i].количество, comboBox.SelectedIndex + 1, dv);
                        else flag = MainWindow.conection.new_Order3(MainWindow.bsk.basket[i].код, MainWindow.bsk.basket[i].количество, Convert.ToInt32(id_client.Text), comboBox.SelectedIndex + 1, dv);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!");
                    }
                    }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            if (flag) this.Close();
        }       

        private void new_client_Click(object sender, RoutedEventArgs e)
        {
            New_Client nc = new New_Client();
            nc.Show();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dv = d-(d*MainWindow.conection.dis(comboBox.SelectedIndex+1)/100);
            price.Text = dv.ToString();
        }

    }
}
