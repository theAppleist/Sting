using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.Filters;
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

        public IEnumerable<object> GetRecords(ISelectFilter select, params IFilter[] filters)
        {
            List<object> records = new List<object>();
            using (SqlConnection client = new SqlConnection(connectionString))
            {
                using(SqlCommand command = client.CreateCommand())
                {
                    string baseQuery = string.Format("{0} * FROM {1}", select.GetFilterString(), tableName);
                    foreach (IFilter filter in filters)
                    {
                        baseQuery += string.Format(" {0}", filter.GetFilterString());
                    }
                    command.CommandText = baseQuery;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] fields = new object[reader.FieldCount];
                            reader.GetValues(fields);
                            records.Add(CreateObject(_communicatorType, fields));
                        }
                    }
                }
            }
            return records;
        }

        private object CreateObject(Type type, object[] flatParameters)
        {
            object[] properties = new object[type.GetProperties().Count()];
            int propertiesIndex = 0;
            for (int i = 0; i < flatParameters.Length; i++)
            {
                var property = type.GetProperties().ElementAt(propertiesIndex);
                if (property.GetType().IsPrimitive)
                {
                    properties[propertiesIndex] = flatParameters[i];
                }
                else
                {
                    int count = property.GetType().GetProperties().Count();
                    object[] props = GetObjectsByRange(i, i + count, flatParameters);
                    i += count;
                    properties[propertiesIndex] = CreateObject(property.GetType(), props);
                }
                propertiesIndex++;
            }
            return Activator.CreateInstance(type, properties);
        }

        private object[] GetObjectsByRange(int start, int end, object[] all)
        {
            object[] ret = new object[end - start];
            for (int i = start; i < ret.Length; i++)
            {
                ret[i - start] = all[i];
            }
            return ret;
        }
    }
}
