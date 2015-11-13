using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Communicator
{
    public class TableCommunicationParameters
    {
        public string TableName { get; set; }
        public string ConnectionString { get; set; }

        public TableCommunicationParameters(string tableName, string connectionString)
        {
            TableName = tableName;
            ConnectionString = connectionString;
        }

        
    }
}
