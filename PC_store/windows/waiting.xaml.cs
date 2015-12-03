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
using System.Threading;

namespace PC_store
{
    /// <summary>
    /// Логика взаимодействия для waiting.xaml
    /// </summary>
    public partial class waiting : Window
    {
        Action act;
        bool _AppClosed = false;
        Thread wait;

        public waiting()
        {
            InitializeComponent();
            App.Current.Exit += ExitApp;
            Closed += Waiting_Closed; 
        }

        private void Waiting_Closed(object sender, EventArgs e)
        {
            _AppClosed = true;
        }

        private void ExitApp(object sender, ExitEventArgs e)
        {
            _AppClosed = true;
        }

        private void waiting_Loaded(object sender, RoutedEventArgs e)
        {
            wait = new Thread(animation);
            wait.Start();
            App.Current.Exit += ThreadStop;
            Closed += Waiting_Closed1; 
        }

        private void Waiting_Closed1(object sender, EventArgs e)
        {
            wait.Abort();
        }

        private void ThreadStop(object sender, ExitEventArgs e)
        {
            wait.Abort();
        }

        void animation()
        {
            while (true)
            {
                for(int i = 0; i < 5; i++)
                {
                    if (_AppClosed)
                    {
                        break;
                    }
                    else
                    {
                        act = () =>
                        {
                            Processline.Text = FillLine(i);
                        };
                        Dispatcher.Invoke(act);
                        Thread.Sleep(750);
                    }
                }
            }
        }

        string FillLine(int n)
        {
            string temp ="";
            for(int i = 0;i < n;i++)
            {
                temp += " |";
            }
            return temp;
        }
    }
}
