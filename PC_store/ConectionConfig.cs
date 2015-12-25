using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_store
{
    [Serializable]
    public class ConectionConfig
    {
        public ConectionConfig(bool IS,string server, string database)
        {
            this.DataBase = database;
            this.Server = server;
            if (IS)
                IntegratedSecurity = true;
            this.SavingConnectionString = true;
            
        }

        public ConectionConfig(bool IS, string server, string database,string user,string password)
        {
            this.Server = server;
            this.DataBase = database;
            if (!IS)
            {
                IntegratedSecurity = false;
                this.User = user;
                this.Password = password;
            }
            this.SavingConnectionString = true;
        }

        public string ConnectionString
        {
            get
            {
                return (IntegratedSecurity) ?
                     "Data source=PEKA\\SQLEXPRESS; Initial catalog=PC_store; Integrated Security=true" :
                     "Server=" + this.Server + "; Database = " + this.DataBase + "; User Id = " + this.User + "; Password = " + this.Password + ";";
            }
        }
        public bool SavingConnectionString { get; set; }
        public bool IntegratedSecurity { get; }

        public string Server { get; set; }
        public string DataBase { get; set; }

        //Значения при проверки подлиности SQL
        public string User { get; set; }
        public string Password { get; set; }
    }
}
