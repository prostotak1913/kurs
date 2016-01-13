using PC_store.Data;
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
    /// Логика взаимодействия для delete_dis.xaml
    /// </summary>
    public partial class delete_dis : Window
    {
        public delete_dis()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
            discount.ItemsSource = MainWindow.conection.fill_dis();
            using (var context = new PC_storeContex())
            {
                dataGrid.ItemsSource=(from d in context.Акции select new { ID = d.ID, Название = d.Название, Скидка = d.Скидка }).ToList();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
                MainWindow.conection.DelDiscount(discount.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
                flag = false;
            }
            if (flag) this.Close();
        }
    }
}
