using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleBase.ViewModel
{
    using Model;
using System.Windows.Input;


    class LoginVM : ObservedObject
    {
        private LoginData loginData = new LoginData();


        public string server
        {
            get { return loginData.Server; }
            set { loginData.Server = value; onPropertyChanged("server"); }
        }

        public string login
        {
            get { return loginData.Login; }
            set { loginData.Login = value; onPropertyChanged("login"); }
        }

        public string password
        {
            get { return loginData.Password; }
            set { loginData.Password = value; onPropertyChanged("password"); }
        }

        public string message
        {
            get { return loginData.Message; }
        }


        private ICommand loginCmd;
        public ICommand LoginCmd
        {
            get
            {
                if (loginCmd == null) loginCmd = new RelayCommand(
                    (object o) =>
                    {
                        loginData.TryLogin();
                        onPropertyChanged("message");
                    },
                    (object o) =>
                    {
                        return server.Length > 0 
                            && login.Length > 0 
                            && password.Length > 0;
                    });
                return loginCmd;
            }
        }
    }
}
