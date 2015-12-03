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
    /// Логика взаимодействия для new_emp.xaml
    /// </summary>
    public partial class New_Emp : Window
    {
        public New_Emp()
        {
            InitializeComponent();
            pos.ItemsSource = MainWindow.conection.fill_Pos();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
        }

        private void new_emp_btm_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
                MainWindow.conection.AddEmploye(name.Text,Convert.ToDouble(salary.Text),pos.SelectedIndex+1,log.Text,pas.Text,date.Text); 
            }
            catch(Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message,"Ошибка!");
            }
            if (flag) this.Close();
        }
    }
}
