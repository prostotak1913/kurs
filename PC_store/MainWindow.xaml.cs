using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_store
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Conection conection = new Conection();
        public static Basket bsk = new Basket();
        public static ConectionConfig configuration;
        waiting w = new waiting();

        public MainWindow()
        {
            InitializeComponent();

            conection.StartConectionProcess += Conection_StartConectionProcess;
            conection.CloseConectionProcess += Conection_CloseConectionProcess;
            conection.DeAuth += Conection_DeAuth;
            Main.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) => { DragMove(); };
            if (File.Exists("server.set"))
            {
                var formatter = new BinaryFormatter();
                var file = new FileStream("server.set", FileMode.Open, FileAccess.Read);
                configuration = formatter.Deserialize(file) as ConectionConfig;
                file.Close();
                if (configuration.SavingConnectionString)
                    if (configuration.IntegratedSecurity)
                        MainWindow.conection.ConectionToServer(configuration.Server, configuration.DataBase);
                    else
                        MainWindow.conection.ConectionToServer(configuration.Server, configuration.DataBase, configuration.User, configuration.Password);
            }

            
        }
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            Login l = new Login();
            l.Show();
            l.Closed += AllClosed;
            conection.Auth += Conection_Auth;
        }

        private void Conection_Auth()
        {
            name_user.Text = conection.Name_current_Emp;
            deAuth.Visibility = Visibility.Visible;
            menu.IsEnabled = true;
            Baskcet.IsEnabled = true;
        }

        private void Conection_DeAuth()
        {
            deAuth.Visibility = Visibility.Hidden;
            menu.IsEnabled = false;
            Baskcet.IsEnabled = false;
        }

        private void conect_Click(object sender, RoutedEventArgs e)//открыть окно настройки
        {
            Main.IsEnabled = false;
            Config conf = new Config();
            conf.Show();
            conf.Closed += AllClosed;
        }

        private void Conection_CloseConectionProcess()
        {
            Action act = () =>
            {
                w.Close();
                SignIn.IsEnabled = true;
            };
                Dispatcher.Invoke(act);            
        }

        private void Conection_StartConectionProcess()
        {
           Action act = () =>
           {
               w = new waiting();
               w.Show();
               Main.IsEnabled = false;
               w.Closed += AllClosed;
            };
            Dispatcher.Invoke(act);
        }

        private void AllClosed(object sender, EventArgs e)
        {
            Main.IsEnabled = true;
        }

        private void exit_Click(object sender, RoutedEventArgs e)//закрыть приложение
        {
            App.Current.Shutdown();
        }

        private void deAuth_Click(object sender, RoutedEventArgs e)
        {
            conection.mt_exit();
            name_user.Text = "Ваше Имя";
            deAuth.Visibility = Visibility.Hidden;
            Baskcet.IsEnabled = false;
            SignIn.IsEnabled = false;
        }

        private void new_emp_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            New_Emp ne = new New_Emp();
            ne.Show();
            ne.Closed += AllClosed;
        }

        private void del_emp_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            deleteEmp de = new deleteEmp();
            de.Show();
            de.Closed += AllClosed;
        }
        
        private void new_prod_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            new_product np = new new_product();
            np.Show();
            np.Closed += AllClosed;
        }

        private void Intake_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            intake_to_str i = new intake_to_str();
            i.Show();
            i.Closed += AllClosed;

        }

        private void sale_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            sale_find sf = new sale_find();
            sf.Show();
            sf.Closed += AllClosed;
        }

        private void add_dis_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            new_dis nd = new new_dis();
            nd.Show();
            nd.Closed += AllClosed;
        }

        private void del_dis_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            delete_dis dd = new delete_dis();
            dd.Show();
            dd.Closed += AllClosed;
        }

        private void f_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            Find f = new Find();
            f.Show();
            f.Closed += AllClosed;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            Basket_Window b = new Basket_Window();
            b.Show();
            b.Closed += AllClosed;
        }

        private void output_Click(object sender, RoutedEventArgs e)
        {
            Main.IsEnabled = false;
            output op = new output();
            op.Show();
            op.Closed += AllClosed;
        }
    }
}
