using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleBase.Model
{
    public class InfoTablePos
    {
        public string tableName { get; set; }
        public string columnName { get; set; }
        public int columnId { get; set; }
        public string dataType { get; set; }
        public int dataLength { get; set; }

        public InfoTablePos()
        {
            tableName = "";
            columnName = "";
            columnId = 0;
            dataType = "";
            dataLength = 0;
        }
    }
}
