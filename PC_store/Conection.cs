using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Threading;
using PC_store.Data;
using System.Data.Entity.Core.EntityClient;

namespace PC_store
{
    public class Conection
    {
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
        private SqlConnection server;
        private EntityConnection conect;

        public SqlConnection Server { get { return this.server; } }

        private bool Allow(int AccessLvL)//проверка уровня доступа
        {
            return (GetAccess_lvl > AccessLvL) ? true : false; ;
        }

        public int GetID_Storage
        {
            get
            {
                using (var context = new PC_storeContex(conect,true))
                {
                    return (from o in context.Сотрудники where o.ID == current_client select o.Объект).First();
                }
            }

        }
        public string Name_current_Emp
        {
            get
            {
                using (var context = new PC_storeContex(conect,true))
                {
                    return (from c in context.Сотрудники where c.ID == current_client select c.ФИО).First();
                }
            }
        }
        
        private void EntityConect(SqlConnection sqlcon)
        {
            EntityConnectionStringBuilder entityBuilder = 
                new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = server.ConnectionString;
            entityBuilder.Metadata = @"res://*/Data.DbModel.csdl|
                            res://*/Data.DbModel.ssdl|
                            res://*/Data.DbModel.msl";
            conect = new EntityConnection(entityBuilder.ToString());
        }

        public void ConectionToServer(string ser, string db, string id, string pass)//Соединение с сервером
        {
            //ConectionString = "Data source=PEKA\\SQLEXPRESS; Initial catalog=PC_store; Integrated Security=true";
            ConectionString = "Server=" + ser + "; Database = " + db + "; User Id = " + id + "; Password = " + pass + ";";
            server = new SqlConnection(ConectionString);
            StartConectionProcess();
            Thread ConectingToSer = new Thread(() =>
              {
                  try
                  {
                      server.Open();
                  }
                  catch
                  {
                      MessageBox.Show("Ошибка подключения к серверу", "Ошибка!");
                  }
                  CloseConectionProcess();
              });
            try
            {
                ConectingToSer.Start();

            }
            catch (SqlException)
            {
                MessageBox.Show("Ошибка подключения к серверу!", "Ошибка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            server.Close();

        }

        public void ConectionToServer(string ser, string db)//Соединение с сервером
        {
            ConectionString = "Data source=" + ser + "; Initial catalog=" + db + "; Integrated Security=true";
            server = new SqlConnection(ConectionString);
            StartConectionProcess();

            try
            {
                Thread ConectingToSer = new Thread(() =>
                {
                    try
                    {
                        server.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка подключения к серверу!", "Ошибка!");
                    }
                    CloseConectionProcess();
                });
                ConectingToSer.Start();

            }
            catch (SqlException)
            {
                MessageBox.Show("Ошибка подключения к серверу!", "Ошибка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            server.Close();

        }

        public bool authorization(string login, string pass) //авторизация
        {
            EntityConect(server);
            using (var context = new PC_storeContex(conect,true))
            {
                var encryptor = new Cryptography();
                login = encryptor.MD5(login);
                bool tmp = true;
                var command = (from c in context.Сотрудники
                               where c.Логин == login
                               select c).First();
                if (command.Пароль == encryptor.MD5(pass))
                {
                    MessageBox.Show("авторизация прошла успешно", "Подключение");
                    current_client = Convert.ToInt16(command.ID);
                    Auth();
                }
                else
                {
                    MessageBox.Show("авторизация не удалась", "ошибка пароля или логина");
                    tmp = false;
                }
                return tmp;
            }
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
                using (var context = new PC_storeContex(conect,true))
                {
                    return (from o in context.Сотрудники where o.ID == current_client select o.Должность).First();
                }

            }
        }

        public bool AddEmploye(string fullname, double salary, string pos, string Login, string Pass, string date,byte[] photo,string gen,string phone,string addres,int obj)//добавление сотрудника
        {
                if (Allow(3))
                {
                    MessageBox.Show("Не достаточно прав", "Ошибка!");
                    return false;
                }
                else
                {
                var encryptor = new Cryptography();
                var tmp = encryptor.MD5(Login);
                using (var context = new PC_storeContex(conect,true))
                {
                    var command = (from e in context.Сотрудники where e.Логин == tmp select e);
                    if (command.Count() == 0)
                    {
                        var сотрудник = new Сотрудники()
                        {
                            ФИО = fullname,
                            ДатаРождения = Convert.ToDateTime(date),
                            Пол = gen,
                            Телефон = phone,
                            МестоЖительства = addres,
                            Фото = photo,
                            ДатаТрудоустройства = DateTime.Now,
                            Объект = obj,
                            Зарплата = (decimal)salary,
                            Должность = Fill_pos(pos),
                            Логин = encryptor.MD5(Login),
                            Пароль = encryptor.MD5(Pass)
                        };
                        context.Сотрудники.Add(сотрудник);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Данный Логин уже занят!","Ошибка");
                        return false;
                    };
                }
                }


        }

        public bool DelEmp(int id)//удаление сотрудника и БД
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                if (current_client != id)
                    if (Allow(3))
                    {
                        MessageBox.Show("Не достаточно прав", "Ошибка!");
                        return false;
                    }
                    else
                    {
                        var emp = contextt.Сотрудники.Where(e=>e.ID==id).First();
                        contextt.Сотрудники.Remove(emp);
                        contextt.SaveChanges();
                    }
                return true;
            }
        }

        public bool AddDiscount(string name, int dis)//добавление скидки
        {
            if (Allow(3))
            {
                MessageBox.Show("Не достаточно прав", "Ошибка!");
                return false;
            }
            else
            {
                using (var contextt = new PC_storeContex(conect,true))
                {
                    var command = (from п in contextt.Акции where п.Название == name select п);
                    if (command.Count() == 0)
                    {
                        var p = new Акции()
                        {
                            Название = name,
                            Скидка = dis
                        };
                        contextt.Акции.Add(p);
                    }

                    contextt.SaveChanges();
                }
                return true;
            }

        }

        public bool DelDiscount(string name)//удаление скидки
        {

            if (Allow(3))
            {
                MessageBox.Show("Не достаточно прав", "Ошибка!");
                return false;
            }
            else
            {
                using (var context = new PC_storeContex(conect,true))
                {
                    var акция = context.Акции.Where(a => a.Название == name).First();
                    context.Акции.Remove(акция);
                    context.SaveChanges();
                }
            }
            return true;

        }

        /*!*/
        public bool new_product(string name, string spec, double price)//добавление нового продукта
        {

            if (Allow(5))
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {
                using (var contextt = new PC_storeContex(conect,true))
                {
                    var product = (from d in contextt.Продукт where d.Название == name select d);
                    if (product.Count() == 0)
                    {
                        Продукт п = new Продукт() { Название = name, Спецификация = spec, Цена = (decimal)price };
                        contextt.Продукт.Add(п);
                        contextt.SaveChanges();
                    }
                }
                return true;
            }

        }

        /*!*/
        public bool intake(int id_prod, int amount)//поступление на склад
        {
            if (Allow(5))
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {
                using (var contextt = new PC_storeContex(conect,true))
                {
                    var command = (from п in contextt.Продукты where п.Склад == GetID_Storage && п.Продукт == id_prod select п);
                    if (command.Count() > 0)
                        command.First().Количество += amount;
                    else
                    {
                        var p = new Продукты()
                        {
                            Количество = amount,
                            Продукт = id_prod,
                            Склад = GetID_Storage
                        };
                        var pr = contextt.Продукт.Where(o => o.ID == id_prod).Select(o => o.Цена).First();
                        var op = new Операции()
                        {
                            Название = "поступление",
                            Время = DateTime.Now,
                            Сотрудник = current_client,
                            Сумма = pr * amount,
                            Продукт = id_prod
                        };
                        contextt.Операции.Add(op);
                        contextt.Продукты.Add(p);
                    }

                    contextt.SaveChanges();
                    
                }
            }

            return true;
        }


        /*!*/
        public void new_client(string fullname, long phone)//регистрация клиентов
        {
            
                server.Open();
            if (GetAccess_lvl >= 5)
            {
                Console.WriteLine("Не достаточно прав");
                return;
            }
            else
            {
                using (var contextt = new PC_storeContex(conect,true))
                {
                    var clients = (from c in contextt.БазаКлиентов where c.Телефон == phone.ToString() select c).ToList();
                    if (clients.Count() != 0)
                    {
                        MessageBox.Show("Уже есть клиент с таким телефоном");
                        return;
                    }
                    else
                    {
                        var ph = phone.ToString();
                        var client = new БазаКлиентов()
                        {
                            ФИО = fullname,
                            Телефон = ph
                        };
                        contextt.SaveChanges();
                        contextt.БазаКлиентов.Add(client);
                    }
                }
            }
            
        }

        /*!*/
        public bool new_client(string fullname, long phone, string adress)//регистрация клиентов
        {
            if (GetAccess_lvl >= 6)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {
                using (var contextt = new PC_storeContex(conect,true))
                {
                    var clients = (from c in contextt.БазаКлиентов where c.Телефон == phone.ToString() select c).ToList();
                    if (clients.Count() != 0)
                    {
                        MessageBox.Show("Уже есть клиент с таким телефоном");
                        return false;
                    }
                    else
                    {
                        var ph = phone.ToString();
                        var client = new БазаКлиентов()
                        {
                            ФИО = fullname,
                            Телефон = ph
                        };
                        contextt.БазаКлиентов.Add(client);
                        contextt.SaveChanges();
                    }
                    return true;
                }

            }
        }

        public bool new_Order(int id_product, int amount, int id_client, int id_discount, double price)//новый заказ с кленитом и со скидкой
        {

            if (GetAccess_lvl >= 5)
            {
                Console.WriteLine("Не достаточно прав");
                return false;
            }
            else
            {
                //var command = new SqlCommand(
                //    "exec New_Order " + id_product + "," + amount + "," + id_client + "," + id_discount + "," + GetID_Storage + ", " + price, server);
                //var reader = command.ExecuteReader();
                //reader.Read();
                //reader.Close();
                //command = new SqlCommand(
                //    "update customer_base set summ = summ+" + price + " where id_client =" + id_client, server);
                //reader = command.ExecuteReader();
                //reader.Read();
                //reader.Close();
                //command = new SqlCommand(
                //    "update products set amount = amount-" + amount + " where id_product =" + id_product, server);
                //reader = command.ExecuteReader();
                //reader.Read();
                //reader.Close();
                var dis = new Акции();
                int? discount = 0;
                using (var contextt = new PC_storeContex(conect,true))
                {
                    int cl;
                    var Заказ = new Заказы()
                    {
                        Продукт = id_product,
                        Количество = amount,
                        Склад = GetID_Storage,
                        Сотрудник = current_client,

                    };
                    try
                    {
                        cl = contextt.БазаКлиентов.Where(o => o.ID == id_client).Select(o => o.ID).First();
                    }
                    catch (InvalidOperationException)
                    {
                        cl = 1;
                    }
                    Заказ.Клиент = cl;
                    try
                    {
                        dis = contextt.Акции.Where(o => o.Скидка == id_discount).Select(o => o).First();
                    }
                    catch (InvalidOperationException)
                    {
                        discount = 0;
                    }
                    Заказ.Скидка = (discount != null) ? (from d in contextt.Акции where d.ID == id_discount select d.Скидка).First() : 0;
                    if (cl != 1)
                    {
                        var client = contextt.БазаКлиентов.Where(c => c.ID == cl).Select(c => c).First();
                        client.сумма +=
                            (double)contextt.Продукт.Where(o => o.ID == id_product).Select(o => o.Цена).First() * amount;
                    }
                    contextt.Заказы.Add(Заказ);
                    contextt.SaveChanges();
                 
                    var sum = (double)contextt.Продукт.Where(o => o.ID == id_product).Select(o => o.Цена).First() * amount;
                    var i = new Операции()
                    {
                        Заказ = (from o in contextt.Заказы select o.ID).ToList().Max(),
                        Время = DateTime.Now,
                        Продукт = id_product,
                        Сотрудник = current_client,
                        Сумма = (decimal)(sum - sum * dis.Скидка / 100),
                        Название = "продажа"

                    };
                    contextt.Операции.Add(i);
                    

                    var products = contextt.Продукты.Where(p => p.Продукт == id_product && p.Склад==GetID_Storage).Select(p => p).First();
                    products.Количество -= amount;
                    contextt.SaveChanges();
                    return true;

                }
            }
            
        }

        //public bool new_Order2(int id_product, int amount, int id_client, double price)//новый заказ с клиентом
        //{
        //    using (var server = new SqlConnection(ConectionString))
        //    {
        //        server.Open();
        //        if (GetAccess_lvl >= 6)
        //        {
        //            Console.WriteLine("Не достаточно прав");
        //            return false;
        //        }
        //        else
        //        {
        //            var command = new SqlCommand(
        //                "exec New_Order " + id_product + "," + amount + "," + id_client + ",null ," + GetID_Storage + ", " + price, server);
        //            var reader = command.ExecuteReader();
        //            reader.Read();
        //            reader.Close();
        //            command = new SqlCommand(
        //                "update customer_base set summ = summ+" + price + " where id_client =" + id_client, server);
        //            reader = command.ExecuteReader();
        //            reader.Read();
        //            reader.Close();
        //            command = new SqlCommand(
        //                "update products set amount = amount-" + amount + " where id_product =" + id_product, server);
        //            reader = command.ExecuteReader();
        //            reader.Read();
        //            return true;
        //        }
        //    }
        //}

        //public bool new_Order(int id_product, int amount, double price)//новый заказ без всего
        //{
        //    using (var server = new SqlConnection(ConectionString))
        //    {
        //        server.Open();
        //        if (GetAccess_lvl >= 6)
        //        {
        //            Console.WriteLine("Не достаточно прав");
        //            return false;
        //        }
        //        else
        //        {
        //            var command = new SqlCommand(
        //                "exec New_Order " + id_product + "," + amount + ", null ,null ," + GetID_Storage + ", " + price, server);
        //            var reader = command.ExecuteReader();
        //            reader.Read();
        //            reader.Close();
        //            command = new SqlCommand(
        //                "update products set amount = amount-" + amount + " where id_product =" + id_product, server);
        //            reader = command.ExecuteReader();
        //            reader.Read();
        //            return true;
        //        }

        //    }
        //}

        //public bool new_Order1(int id_product, int amount, int id_dis, double price)//новый заказ со скидкой
        //{

        //    using (var server = new SqlConnection(ConectionString))
        //    {
        //        server.Open();
        //        if (GetAccess_lvl >= 6)
        //        {
        //            Console.WriteLine("Не достаточно прав");
        //            return false;
        //        }
        //        else
        //        {
        //            var command = new SqlCommand(
        //                "exec New_Order " + id_product + "," + amount + ", null, " + id_dis + ", " + GetID_Storage + ", " + price, server);
        //            var reader = command.ExecuteReader();
        //            reader.Read();
        //            reader.Close();
        //            command = new SqlCommand(
        //                "update products set amount = amount-" + amount + " where id_product =" + id_product, server);
        //            reader = command.ExecuteReader();
        //            reader.Read();
        //            return true;
        //        }
        //    }
        //}

        public List<Продукт> find_prod(string name)//поиск товара
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Продукт where p.Название == name select p).ToList();
            }
        }

        public List<string> fill_Pos()//поиск должностей
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Должности select p.Назавние).ToList();
            }
        }

        public List<string> fill_Prod()//поиск продуктов
        {
            using (var context = new PC_storeContex(conect,true))
            {
                return (from p in context.Продукт select p.Название).ToList();

            }
        }

        public List<string> fill_dis()//поиск скидки
        {
            using (var context = new PC_storeContex(conect,true))
            {
                return (from d in context.Акции select d.Название).ToList();
            }
        }

        public double find_prod(int n)//поиск цены продукта по его номеру
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (double)(from p in contextt.Продукт where p.ID == n select p.Цена).First();
            }
        }

        public bool availeble(int i, int a)//доступность товара на текущем складе
        {
            using (var context = new PC_storeContex(conect,true))
            {
                bool tmp = true;
                var command = (from пр in context.Продукты where пр.Продукт == i && пр.Склад == GetID_Storage && пр.Количество >= a select пр).First();
                if (command == null) tmp = false;
                else tmp = true;

                return tmp;
            }
        }

        public int dis(int id)
        {
            using (var context = new PC_storeContex(conect,true))
            {
                return (int)(from d in context.Акции where d.ID == id select d.Скидка).First();
            }
        }

        public DataTable Search(string table, string column, string vallue)//поиск в таблице table в столбце column по значению value
        {
            DataTable tmp = new DataTable();
            var command = (vallue.Trim() != "*" && vallue.Trim() != "" && vallue != "...") ?
                new SqlCommand("select * from " + table + " where " + column + " = '" + vallue + "'", server)
                : new SqlCommand("select * from " + table, server);
            command.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tmp);

            return tmp;

        }


        public List<string> fill_CB()
        {

            List<string> lp = new List<string>();
            SqlCommand command = new SqlCommand("use " + server.Database + " select name from sys.objects WHERE type in (N'U') and not(name='sysdiagrams') and not(name ='Сотрудники')", server);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lp.Add(reader["name"].ToString());
            }
            reader.Close();

            return lp;
        }

        public List<string> fill_CB(string s)
        {
            List<string> lp = new List<string>();
            var command = new SqlCommand("use " + server.Database + " SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '" + s + "'", server);
            var reader = command.ExecuteReader();
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
            SqlCommand command1 = new SqlCommand("select * from " + table, server);
            command1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(command1);
            da.Fill(dt);
            return dt;
        }

        public List<int> fillObj()
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from o in contextt.Объекты select o.ID).ToList();
            }
        }

        public int Fill_pos(string name)
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Должности where p.Назавние == name select p.ID).First();
            }
        }

        public List<string> Fill_emp()
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Сотрудники where p.Объект==GetID_Storage select p.ФИО).ToList();
            }
        }

        public int Employe(string name)
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Сотрудники where p.ФИО==name select p.ID).First();
            }
        }

        public Сотрудники Employe(int id)
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Сотрудники where p.ID == id select p).First();
            }
        }

        public string Pos(int i)
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Должности where p.ID == i select p.Назавние).First();
            }
        }

        public string prodName(int id)
        {
            using (var contextt = new PC_storeContex(conect,true))
            {
                return (from p in contextt.Продукт where p.ID == id select p.Название).First();
            }
        }
    }
}


