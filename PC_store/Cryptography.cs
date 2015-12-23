using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PC_store
{
    class Cryptography
    {
        public string MD5(string text)
        {
            var md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            var result = md5.Hash;
            var str = new StringBuilder();
            foreach (var i in result)
                str.Append(i.ToString("x2"));
            return str.ToString();
        }
    }
}
