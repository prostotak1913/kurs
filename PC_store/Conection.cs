using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Threading;

namespace PC_store
{
    public class Conection 
    {
        SqlConnection server = new SqlConnection();
        SqlCommand command;
        SqlDataReader reader;
        string ConectionString;
        int current_client;

        public delegate void authorizating();
        public delegate void deAuthorithating();
        public delegate void exit();
        public delegate void StartCon();
        public delegate void CloseCon();

        public event authorizating Auth;
        public event deAuthorithating DeAuth; 
        public event exit Exit;      
        public event StartCon StartConectionProcess;       
        public event CloseCon CloseConectionProcess;

        public int GetID_Storage
        {
            get
            {
                reader.Close();
                command.CommandText =
                    "select id_storage from storages_outpost where id_emp = '" + current_client + "'";
                reader = command.ExecuteReader();
                reader.Read();
                int id_str = Convert.ToInt32(reader["id_storage"]);
                reader.Close();
                return id_str;
            }
        }

        public string Name_current_Emp
        {
            get
            {
                reader.Close();
                command.CommandText = "select fullname from Employes where id_emp = " + current_client;
                reader = command.ExecuteReader();
                reader.Read();
                return reader["fullname"].ToString();
            }
        }

        public void ConectionToServer(string ser,string db,string id, string pass)//Соединение с сервером
        {
            StartConectionProcess();
            Thread ConectingToSer = new Thread(() =>
              {
                 
                //ConectionString = "Server=" + ser + "; Database = " + db + "; User Id = " + id + "; Password = " + pass + ";";
                ConectionString = "Data source=PEKA\\SQLEXPRESS; Initial catalog=PC_store; Integrated Security=true";
                try
                  {
                      server = new SqlConnection(ConectionString);
                      server.Open();

                  }
                  catch (SqlException)
                  {
                      MessageBox.Show("Ошибка подключения к серверу!", "Ошибка!");
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show(ex.Message, "Ошибка!");
                  }
                  CloseConectionProcess();
              });
            ConectingToSer.Start();
           
            command = server.CreateCommand();
        }

        public Conection()
        {
        }

        public string State
        {
            get { return this.server.State.ToString(); }
        }

        public bool authorization(string login, string pass) //авторизация
        {
            bool tmp = true;
            command = new SqlCommand("select * from log_pas where login_emp = " + "'" + login + "'",server);
            reader = command.ExecuteReader();
            reader.Read();
            try
            {
                if (reader["pass_emp"].ToString() == pass)
                {
                    MessageBox.Show("авторизация прошла успешно", "Подключение");
                    current_client = Convert.ToInt16(reader["id_emp"]);
                    Auth();
                }
                else
                {
                    MessageBox.Show("авторизация не удалась", "ошибка пароля или логина");
                    tmp = false;
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("авторизация не удалась", "ошибка пароля или логина");
                tmp = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка");
                tmp = false;
            }
            reader.Close();
            return tmp;

        }

        public void mt_exit()//Оконание сеанса
        {
            current_client = new int();
            DeAuth();
        }

        private int GetAccess_lvl//возвращает уровень доступа
        {
            get
            {
                reader.Close();
                command.CommandText = "select id_position from Employes where id_emp = " + current_client;
                reader = command.ExecuteReader();
                reader.Read();
                return Convert.ToInt32(reader["id_position"]);
            }
        }

        public void AddEmploye(string fullname, double salary, int pos, string Login, string Pass,string date)//добавление сотрудника
        {

            if (GetAccess_lvl > 3)
            {
                MessageBox.Show("Не достаточно прав","Ошибка!");
                return;
            }
            else
            {
                reader.Close();
                command.CommandText =
                "select * from log_pas where login_emp = " + "'" + Login + "'";
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("пользователь с таким логином уже есть", "Ошибка!");
                    reader.Close();
                    return;
                }
                reader.Close();
                command.CommandText =
                "exec ADDEmp" + "'" + fullname + "', " + Math.Round(salary, 2) + ", " + pos + ", '" + Login + "', " + "'" + Pass + "', '"+date+"'";
                reader = command.ExecuteReader();
                reader.Read() ;
            }
            reader.Close();
        }
        
        public bool DelEmp(int id)//удаление сотрудника и БД
        {
            if (current_client != id)
                if (GetAccess_lvl > 3)
                {
                    MessageBox.Show("Не достаточно прав","Ошибка!");
                    return false;
                }
                else
                {

                    reader.Close();
                    command.CommandText =
                    "delete from Employes where id_emp = " + id;
                    reader = command.ExecuteReader();
                    reader.Read();
                }
            reader.Close();
            return true;
        }

        public void AddStorage(string addres, int id_emp)//добавление Аутпоста
        {
            if (GetAccess_lvl > 2)
            {
                Console.WriteLine("Не достаточно прав");
                return;
            }
            else
            {
                {
                    reader.Close();
                    command.CommandText =
                    "select * from storages_outpost where address = " + "'" + addres + "'";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("объект потакому адресу уже имеется");
                        reader.Close();
                        return;
                    }
                    reader.Close();
                    command.CommandText =
                    "select * from storages_outpost where id_emp = " + id_emp;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("данный сотрудник занят");
                        reader.Close();
                        return;
                    }

                    reader.Close();
                    command.CommandText =
                        "exec add_storage" + "'" + addres + "', " + id_emp;
                    reader = command.ExecuteReader();
                    reader.Read();

                }
                reader.Close();
            }
        }

        public bool AddDiscount(string name, int dis)//добавление скидки
        {
            if (GetAccess_lvl > 3)
            {
                MessageBox.Show("Не достаточно прав","Ошибка!");
                return false;
            }
            else
            {
                {
                    reader.Close();
                    command.CommandText =
                        "select * from discount where name = '" + name + "'";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("данная скидка уже присутствует");
                        reader.Close();
                        return false;
                    }
                    reader.Close();
                    command.CommandText =
                        "exec add_discount" + "'" + name + "', " + dis;
                    reader = command.ExecuteReader();
                    reader.Read();

                }                
                reader.Close();
                return true;
            }
        }

        public bool DelDiscount(int id)//удаление скидки
        {
            if (GetAccess_lvl > 3)
            {
                MessageBox.Show("Не достаточно прав", "Ошибка!");
                return false;
            }
            else
            {
                reader.Close();
                command.CommandText =
                    "delete from discount where id_discount =" + id;
                reader = command.ExecuteReader();
                reader.Read();

            }
            reader.Close();
            return true;
        }      
        
        public bool new_product(string name, string spec, double price)//добавление нового продукта
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {
                {
                    reader.Close();
                    command.CommandText =
                        "select * from discount where name = '" + name + "'";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("данный товар уже присутствует");
                        reader.Close();
                        return false;
                    }
                    reader.Close();
                    command.CommandText =
                        "exec add_product" + "'" + name + "', '" + spec + "'," + price;
                    reader = command.ExecuteReader();
                    reader.Read();

                }
                reader.Close();
                return true;
            }
        }

        public bool intake(int id_prod, int amount)//поступление на склад
        {
            reader.Close();
            command.CommandText = "select id_storage from storages_outpost where id_emp = '" + current_client+"'";
            reader = command.ExecuteReader();
            reader.Read();
            int id_str = Convert.ToInt32(reader["id_storage"]);

            if (GetAccess_lvl >= 6)
            {
                MessageBox.Show("Не достаточно прав","Ошибка!");
                return false;
            }
            else
            {
                reader.Close();
                command.CommandText = "exec intake " + id_prod + ", " + amount + ", " + id_str;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
            }
            return true;

        }

        public void new_client(string fullname, long phone)//регистрация клиентов
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return;
            }
            else
            {
                reader.Close();
                command.CommandText =
                "select * from customer_base where phone = " + phone;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("клиент с таким телефоном уже есть");
                    reader.Close();
                    return;
                }
                reader.Close();
                command.CommandText =
                    "exec new_client " + "'" + fullname + "',null," + phone;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
            }
        }

        public bool new_client(string fullname, long phone, string adress)//регистрация клиентов
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {
                reader.Close();
                command.CommandText =
                "select * from customer_base where phone = " + phone;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Console.WriteLine("клиент с таким телефоном уже есть");
                    reader.Close();
                    return false;
                }
                reader.Close();
                command.CommandText =
                    "exec new_client " + "'" + fullname + "', '" + adress + "'," + phone;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                return true;
            }
        }

        public bool new_Order3(int id_product, int amount, int id_client, int id_discount,double price)//новый заказ с кленитом и со скидкой
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {


                reader.Close();
                command.CommandText =
                    "exec New_Order " + id_product + "," + amount + "," + id_client + "," + id_discount + "," + GetID_Storage + ", " + price;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                command.CommandText =
                    "update customer_base set summ = summ+" + price + " where id_client =" + id_client;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                command.CommandText =
                    "update products set amount = amount-" + amount + " where id_product =" + id_product;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                return true;
            }
        }

        public bool new_Order2(int id_product, int amount, int id_client, double price)//новый заказ с клиентом
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {

                reader.Close();
                command.CommandText =
                    "exec New_Order " + id_product + "," + amount + "," + id_client + ",null ," + GetID_Storage + ", " + price;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                command.CommandText =
                    "update customer_base set summ = summ+" + price + " where id_client =" + id_client;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                command.CommandText =
                    "update products set amount = amount-" + amount + " where id_product =" + id_product;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                return true;
            }
        }

        public bool new_Order(int id_product, int amount,double price)//новый заказ без всего
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {


                reader.Close();
                command.CommandText =
                    "exec New_Order " + id_product + "," + amount + ", null ,null ," + GetID_Storage + ", " + price;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                command.CommandText =
                    "update products set amount = amount-" + amount + " where id_product =" + id_product;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                return true;
            }
        }

        public bool new_Order1(int id_product, int amount, int id_dis,double price)//новый заказ со скидкой
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {

                reader.Close();
                command.CommandText =
                    "exec New_Order " + id_product + "," + amount + ", null, " + id_dis + ", " + GetID_Storage + ", " + price;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                command.CommandText =
                    "update products set amount = amount-" + amount + " where id_product =" + id_product;
                reader = command.ExecuteReader();
                reader.Read();
                reader.Close();
                return true;
            }
        }
        
        public DataTable find_prod(string name)//поиск товара
        {

            SqlCommand command1 = new SqlCommand("select * from product where name = '" + name + "'", server);
            DataTable dt = new DataTable("product");
            command1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(command1);
            da.Fill(dt);
            reader.Close();

            reader.Close();
            return dt;
        }

        public List<string> fill_Pos()//поиск должностей
        {
            List<string> lp = new List<string>();
            SqlCommand command1 = new SqlCommand("select name from Position",server);
            reader = command1.ExecuteReader();
            while(reader.Read())
            {
                lp.Add(reader["name"].ToString());
            }
            reader.Close();
            return lp;
           
        }

        public List<string> fill_Prod()//поиск продуктов
        {
            List<string> lp = new List<string>();
            SqlCommand command1 = new SqlCommand("select name from product", server);
            reader = command1.ExecuteReader();
            while (reader.Read())
            {
                lp.Add(reader["name"].ToString());
            }
            reader.Close();
            return lp;

        }

        public List<string> fill_dis()//поиск скидки
        {
            reader.Close();
            List<string> lp = new List<string>();
            SqlCommand command1 = new SqlCommand("select name from discount", server);
            reader = command1.ExecuteReader();
            while (reader.Read())
            {
                lp.Add(reader["name"].ToString());
            }
            reader.Close();
            return lp;
        }

        public double find_prod(int n)//поиск цены продукта по его номеру
        {
            reader.Close();
            command = new SqlCommand("select price from product where id_product = " + n,server);
            reader = command.ExecuteReader();
            reader.Read();
            return Convert.ToDouble(reader["price"]);
        }

        public bool availeble(int i,int a)//доступность товара на текущем складе
        {
            bool tmp =true;

            reader.Close();
            command.CommandText = "select amount from products where id_product = " + i + " and id_storage=" + GetID_Storage;
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows) tmp = false;
            else
            {
                if (a > Convert.ToInt32(reader["amount"])) return false;
                else tmp = true;
            }
            return tmp;
        }

        public int dis(int id)
        {
            reader.Close();
            command = new SqlCommand("select discount from discount where id_discount = " + id, server);
            reader = command.ExecuteReader();
            reader.Read();
            return Convert.ToInt32(reader["discount"]);
        }

        public DataTable Search(string table, string column, string vallue)//поиск в таблице table в столбце column по значению value
        {
            DataTable tmp = new DataTable();
            SqlCommand command1 = (vallue.Trim() != "*" && vallue.Trim() != "" && vallue != "...") ? new SqlCommand("select * from " + table + " where " + column + " = '" + vallue + "'", server)
            : new SqlCommand("select * from " + table, server);
            command1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(command1);
            da.Fill(tmp);
            return tmp; 
        }

        public List<string> fill_CB()
        {
            reader.Close();
            List<string> lp = new List<string>();
            SqlCommand command1 = new SqlCommand("use "+server.Database+ " select name from sys.objects WHERE type in (N'U') and not(name='sysdiagrams')", server);
            reader = command1.ExecuteReader();
            while (reader.Read())
            {
                lp.Add(reader["name"].ToString());
            }
            reader.Close();
            return lp;
        }

        public List<string> fill_CB(string s)
        {
            reader.Close();
            List<string> lp = new List<string>();
            SqlCommand command1 = new SqlCommand("use " + server.Database + " SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '"+s+"'", server);
            reader = command1.ExecuteReader();
            while (reader.Read())
            {
                lp.Add(reader["COLUMN_NAME"].ToString());
            }
            reader.Close();
            return lp;
        }

        public DataTable OutPut(string table)
        {
            DataTable dt = new DataTable();
            SqlCommand command1 = new SqlCommand("select * from " + table,server);
            command1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(command1);
            da.Fill(dt);
            return dt;
        }
    }

}
