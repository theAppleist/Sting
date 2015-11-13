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
        public IEnumerable<string> Columns { get; set; }

        public TableCommunicationParameters(string tableName, string connectionString, IEnumerable<string> columns)
        {
            TableName = tableName;
            ConnectionString = connectionString;
            Columns = columns;
        }

        
    }
}
