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
    /// Логика взаимодействия для find_prod.xaml
    /// </summary>
    public partial class find_prod : Window
    {
        public find_prod()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
            find.Click += (object sender, RoutedEventArgs e) => { dataGrid.ItemsSource = MainWindow.conection.find_prod(name.Text).DefaultView; };
        }
    }
}
