using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.Filter;
using System.Data.SqlClient;

namespace DAL.CommunicatorImplemenatations
{
    public class ReadCommunicator : TableCommunicator, IReadCommunicator
    {
        private Type _communicatorType;

        public ReadCommunicator(TableCommunicationParameters parameters, Type modelType)
            :base(parameters)
        {
            _communicatorType = modelType;
        }

        public IEnumerable<string> GetColumns()
        {
            return columns;
        }

        public Type GetCommunicatorModelType()
        {
            return _communicatorType;
        }

        public IEnumerable<object> GetRecords(IFilter filter)
        {
            List<object> records = new List<object>();
            using (SqlConnection client = new SqlConnection(connectionString))
            {
                using(SqlCommand command = client.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM {0} {1}", tableName, filter.GetFilterString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] fields = new object[reader.FieldCount];
                            reader.GetValues(fields);
                            records.Add(Activator.CreateInstance(_communicatorType, fields));
                        }
                    }
                }
            }
            return records;
        }
    }
}
