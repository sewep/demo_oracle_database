using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleBase.ViewModel
{
    using Model;
    using System.Windows.Input;
    using System.Windows.Media;


    public class LoginVM : ObservedObject
    {
        private LoginData loginData = new LoginData();
        private DataBase dataBase;

        public LoginVM()
        {
            dataBase = new DataBase(loginData);
        }


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

        public Brush messageColor
        {
            get
            {
                Color c;
                if (dataBase.isConnected) c = Color.FromArgb(255, 0, 155, 0);
                else if (dataBase.errorConnecting) c = Color.FromArgb(255, 225, 0, 0);
                else c = Color.FromArgb(255, 0, 0, 0);
                return new SolidColorBrush(c);
            }
        }


        private ICommand loginCmd;
        public ICommand LoginCmd
        {
            get
            {
                if (loginCmd == null) loginCmd = new RelayCommand(
                    (object o) =>
                    {
                        dataBase.TryConnect();
                        onPropertyChanged("message");
                        onPropertyChanged("messageColor");
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
