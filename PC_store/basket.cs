using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace PC_store
{
    public class product
    {
        public product(int c,int a)
        {
            код = c;
            количество = a;
        }

        public int номер { get; }
        public int код { get; }
        public int количество { get; }
        public double цена
        {
            get
            {
                double temp = MainWindow.conection.find_prod(код);
                return (temp * количество);
            }
        }
    }

    public class Basket
    {
        List<product> b = new List<product>();
        public List<product> basket
        {
            get { return this.b; }
        }  

        public void add(int c,int a)
        {
            b.Add(new product(c, a));
        }

        public void del(int n)
        {
            b.RemoveAt(n);
        }

    }
}
