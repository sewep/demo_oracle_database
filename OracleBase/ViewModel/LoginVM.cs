using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleBase.Model;

namespace OracleBase.ViewModel
{
    using Model;
    using System.Windows.Input;
    using System.Windows.Media;
    using OracleBase.Model.DBase;


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
            set { loginData.Message = value; }
        }

        
        public Brush messageColor
        {
            get
            {
                Color c = Color.FromArgb(255, 0, 0, 0);
                if (loginData.TestConnectionSucces == true) c = Color.FromArgb(255, 0, 175, 0);
                if (loginData.TestConnectionSucces == false) c = Color.FromArgb(255, 225, 0, 0);
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
                        SqlBase.loginData = loginData; // Reference to connection info
                        var db = new TestConnection();
                        db.Test();
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
