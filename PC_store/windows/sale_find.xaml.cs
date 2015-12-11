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
    /// Логика взаимодействия для sale_find.xaml
    /// </summary>
    public partial class sale_find : Window
    {
        public sale_find()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) =>{ this.Close(); };
            header.MouseLeftButtonDown+=(object sender, MouseButtonEventArgs e) =>{ DragMove(); };
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {
            find_prod fp = new find_prod();
            fp.Show();

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.conection.availeble(Convert.ToInt32(id_prod.Text), Convert.ToInt32(amount.Text))) MainWindow.bsk.add(Convert.ToInt32(id_prod.Text), Convert.ToInt32(amount.Text));
                else MessageBox.Show("Данный продукт отсутвуетна складе в таком количестве, или его - нет", "Ошибка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}