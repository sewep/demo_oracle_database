using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleBase.Model.DBase
{
    class TestConnection : SqlBase
    {
        public bool Test()
        {
            bool isOk = true;
            if (Connect())
            {
                loginData.Message = "Connection successful.";
            }
            else
            {
                loginData.Message = "Connection error: " + conErrorMsg;
                isOk = false;
            }

            Close();
            loginData.TestConnectionSucces = isOk;
            return isOk;
        }
    }
}
