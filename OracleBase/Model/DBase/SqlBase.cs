using Devart.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleBase.Model.DBase
{
    public class SqlBase
    {
        public static LoginData loginData;
        OracleConnection oracleConnection;
        protected string conErrorMsg = "";

        protected bool Connect()
        {
            try
            {
                var cstr = new OracleConnectionStringBuilder();
                cstr.Server = loginData.Server;
                cstr.UserId = loginData.Login;
                cstr.Password = loginData.Password;
                oracleConnection = new OracleConnection(cstr.ConnectionString);
                oracleConnection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                conErrorMsg = ex.Message;
                return false;
            }
            return true;
        }

        protected void Close()
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
