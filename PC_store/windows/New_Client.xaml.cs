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
    /// Логика взаимодействия для New_Client.xaml
    /// </summary>
    public partial class New_Client : Window
    {
        public New_Client()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            bool flag;
            flag = MainWindow.conection.new_client(fullname.Text, Convert.ToInt64(phone.Text), textBox.Text);
            if (flag) this.Close();
        }

    }
}