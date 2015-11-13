using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Communicator
{
    public  class TableCommunicationparameters
    {
        public string TableName { get; set; }
        public string ConnectionString { get; set; }

        public TableCommunicationparameters(string tableName, string connectionString)
        {
            TableName = tableName;
            ConnectionString = connectionString;
        }

        
    }
}
