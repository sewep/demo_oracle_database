using Devart.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleBase.Model
{
    class DataBase
    {
        static DataBase instance = null;
        OracleConnection oracleConnection;
        LoginData loginData;
        public bool errorConnecting = false;
        public bool isConnected = false;

        public DataBase(LoginData loginData)
        {
            if (instance != null) 
                instance.CloseConnection(); // Clear previus instance if exist

            instance = this;
            this.loginData = loginData;
        }

        public static DataBase Instance
        {
            get { return instance; }
        }

        public bool TryConnect()
        {
            CloseConnection();

            loginData.Message = "Connecting...";
            isConnected = false;
            errorConnecting = false;
            try
            {
                var cstr = new OracleConnectionStringBuilder();
                cstr.Server = loginData.Server;
                cstr.UserId = loginData.Login;
                cstr.Password = loginData.Password;
                oracleConnection = new OracleConnection(cstr.ConnectionString);
                oracleConnection.Open();
            } catch (Exception ex)
            {
                loginData.Message = "Connecting error: " + ex.Message;
                errorConnecting = true;
                return false;
            }
            loginData.Message = "Connected.";
            isConnected = true;
            return true;
        }

        public void CloseConnection()
        {
            if (oracleConnection == null) return;
            try
            {
                oracleConnection.Close();
                oracleConnection = null;
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
