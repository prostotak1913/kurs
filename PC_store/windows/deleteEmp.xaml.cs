using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для deleteEmp.xaml
    /// </summary>
    public partial class deleteEmp : Window
    {
        int id_emp;
        public deleteEmp()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
            emp.ItemsSource = MainWindow.conection.Fill_emp();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
               flag=MainWindow.conection.DelEmp(id_emp);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка!");
                flag = false;
            }
            if (flag) this.Close();
        }

        private void emp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            id_emp = MainWindow.conection.Employe(emp.SelectedValue.ToString());
            name.Text = MainWindow.conection.Employe(id_emp).ФИО;
            gen.Text = MainWindow.conection.Employe(id_emp).Пол;
            date.Text = MainWindow.conection.Employe(id_emp).ДатаРождения.ToString();
            addres.Text = MainWindow.conection.Employe(id_emp).МестоЖительства;
            phone.Text = MainWindow.conection.Employe(id_emp).Телефон;
            Pos.Text = MainWindow.conection.Pos(MainWindow.conection.Employe(id_emp).Должность);
            salary.Text = MainWindow.conection.Employe(id_emp).Зарплата.ToString()+"р.";
            TimeSpan interval = DateTime.Now - MainWindow.conection.Employe(id_emp).ДатаТрудоустройства;
            exp.Text = Math.Round(interval.TotalDays).ToString()+" дней";
            BitmapImage bmi = new BitmapImage();
            if (MainWindow.conection.Employe(id_emp).Фото != null)
            {
                bmi.BeginInit();
                bmi.StreamSource = new MemoryStream(MainWindow.conection.Employe(id_emp).Фото);
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();
                image.Source = bmi;
            }
        }
    }
}
