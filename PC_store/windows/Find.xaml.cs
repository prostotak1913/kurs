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
    /// Логика взаимодействия для Find.xaml
    /// </summary>
    public partial class Find : Window
    {
        public Find()
        {
            InitializeComponent();
            tab.ItemsSource = MainWindow.conection.fill_CB();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };

        }

        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            col.IsEnabled = true;
            col.ItemsSource = null;
            col.ItemsSource = MainWindow.conection.fill_CB(tab.SelectedValue.ToString());
        }

        private void col_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search_text.IsEnabled = true;
            search.IsEnabled = true;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGrid.ItemsSource = MainWindow.conection.Search(tab.SelectedValue.ToString(), col.SelectedValue.ToString(), Search_text.Text).DefaultView;
                col.ItemsSource = null;
                col.IsEnabled = false;
                Search_text.IsEnabled = false;
                search.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            col.ItemsSource = null;
            dataGrid.ItemsSource = null;
            col.IsEnabled = false;
            Search_text.IsEnabled = false;
            search.IsEnabled = false;
            Search_text.Text = "...";

        }
    }
}
