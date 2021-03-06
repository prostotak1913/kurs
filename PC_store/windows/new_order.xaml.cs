﻿using System;
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
            int dis = 0;
            if (id_client.Text == "№ клиента" || id_client.Text == "") id_client.Text = "1";
            if (comboBox.SelectedIndex == -1) dis = 0; 
            try
            {
                for (int i = 0; i < MainWindow.bsk.basket.Count; i++)
                {
                    try
                    { 
                        flag = MainWindow.conection.new_Order(MainWindow.bsk.basket[i].код, MainWindow.bsk.basket[i].количество, Convert.ToInt32(id_client.Text),dis, MainWindow.bsk.basket[i].цена);

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
