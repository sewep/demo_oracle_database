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

        public List<InfoTablePos> getInfoTables()
        {
            if (oracleConnection == null || !isConnected)
                return null;

            var sql = @"select col.table_name, 
                               col.column_name,
	                           col.column_id,  
                               col.data_type, 
                               col.data_length
                        from sys.all_tab_columns col
                        inner join sys.all_tables t on col.owner = t.owner 
                                                      and col.table_name = t.table_name
                        where col.owner = 'DEV_DATA_1' and col.data_type = 'NUMBER'
                        order by col.table_name, col.column_name";
            OracleCommand myCommand = new OracleCommand(sql, oracleConnection);
            OracleDataReader reader = myCommand.ExecuteReader();

            var list = new List<InfoTablePos>();
            try
            {
                while (reader.Read())
                {
                    list.Add(new InfoTablePos() {
                        tableName = reader.GetString(0),
                        columnName = reader.GetString(1),
                        columnId = reader.GetInt32(2),
                        dataType = reader.GetString(3),
                        dataLength = reader.GetInt32(4)
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return list;
        }
    }
}
