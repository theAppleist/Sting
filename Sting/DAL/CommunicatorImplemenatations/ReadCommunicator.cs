using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.Filters;
using System.Data.SqlClient;

namespace DAL.CommunicatorImplemenatations
{
    public class ReadCommunicator : Communicator.TableCommunicator, IReadCommunicator
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

        public IEnumerable<object> GetRecords(ISelectFilter select, IFilter valuesFilter, params IFilter[] filters)
        {
            List<object> records = new List<object>();
            if (valuesFilter == null)
            {
                valuesFilter = new ValueFilter("*");
            }
            using (SqlConnection client = new SqlConnection(connectionString))
            {
                client.Open();
                using(SqlCommand command = client.CreateCommand())
                {
                    string baseQuery = string.Format("{0} {1} FROM {2}", select.GetFilterString(), valuesFilter.GetFilterString(), tableName);
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
                var t = property.PropertyType; 
                if (!property.PropertyType.IsSubclassOf(typeof(StingCore.StingAbstractModel)))
                {
                    properties[propertiesIndex] = flatParameters[i];
                }
                else
                {
                    int count = CalculateTotalCount(property.PropertyType);
                    object[] props = GetObjectsByRange(i, i + count, flatParameters);
                    i += count-1;
                    properties[propertiesIndex] = CreateObject(property.PropertyType, props);
                }
                propertiesIndex++;
            }
            return Activator.CreateInstance(type, properties);
        }

        private object[] GetObjectsByRange(int start, int end, object[] all)
        {
            object[] ret = new object[end - start];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = all[i+start];
            }
            return ret;
        }

        private int CalculateTotalCount(Type type)
        {
            int sum = 0;
            foreach (var property in type.GetProperties())
            {
                if (!property.PropertyType.IsSubclassOf(typeof(StingCore.StingAbstractModel)))
                {
                    ++sum;
                }
                else
                {
                    sum += CalculateTotalCount(property.PropertyType) ;
                }
            }
            return sum;
        }
    }
}
