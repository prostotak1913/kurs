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
    /// Логика взаимодействия для output.xaml
    /// </summary>
    public partial class output : Window
    {
        public output()
        {
            InitializeComponent();
            tab.ItemsSource = MainWindow.conection.fill_CB();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
            tab.SelectionChanged += (object sender, SelectionChangedEventArgs e) => 
            {
                dataGrid.ItemsSource = MainWindow.conection.OutPut(tab.SelectedValue.ToString()).DefaultView; }
            ;
        }

    }
}
