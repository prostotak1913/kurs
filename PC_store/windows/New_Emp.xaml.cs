using Microsoft.Win32;
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
using System.IO;

namespace PC_store
{
    /// <summary>
    /// Логика взаимодействия для new_emp.xaml
    /// </summary>
    public partial class New_Emp : Window
    {
        BitmapImage bm1;
        string photoUrl;
        public New_Emp()
        {
            InitializeComponent();
            exit.Click += (object sender, RoutedEventArgs e) => { this.Close(); };
            header.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
            gen.ItemsSource = new List<string>() { "М", "Ж" };
            obj.ItemsSource = MainWindow.conection.fillObj();
            pos.ItemsSource = MainWindow.conection.fill_Pos();

        }

        private void new_emp_btm_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            try
            {
                var fs = new FileStream(photoUrl, FileMode.Open, FileAccess.Read);
                var br = new BinaryReader(fs);
                byte[] Photo = br.ReadBytes((int)fs.Length);
                flag = MainWindow.conection.AddEmploye(name.Text,Convert.ToDouble(salary.Text),(string)pos.SelectedValue,log.Text,pas.Text,date.Text,Photo,(string)gen.SelectedValue,phone.Text,addres.Text,(int)obj.SelectedValue); 
            }
            catch(Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message,"Ошибка!");
            }
            if (flag) this.Close();
        }

        private void photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                photoUrl = dlg.FileName;

                bm1 = new BitmapImage();
                bm1.BeginInit();
                bm1.UriSource = new Uri(photoUrl, UriKind.Relative);
                bm1.CacheOption = BitmapCacheOption.OnLoad;
                bm1.EndInit();
                image.Source = bm1;
            }
        }
    }
}
