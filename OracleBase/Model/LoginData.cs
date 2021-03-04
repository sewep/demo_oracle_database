using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleBase.Model
{
    public class LoginData
    {
        public string Server = "localhost";
        public string Login = "DEV_DATA_1";
        public string Password = "";
        public string Message = "Enter your login details and click Test connection.";

        public void TryLogin()
        {
            Message = "Try to login...";
        }
    }
}
