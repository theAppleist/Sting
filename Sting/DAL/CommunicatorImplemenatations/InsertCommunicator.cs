using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.Filters;

namespace DAL.CommunicatorImplemenatations
{
    //refacctor becuse need to have columns in the order of properties
    public class InsertCommunicator :TableCommunicator, IInsertCommuncitor
    {
        private readonly string InsertCommand = "INSERT INTO {0} OUTPUT INSERTED.ID VALUES ({1})";  
        
        public InsertCommunicator(TableCommunicationParameters paramerters)
            :base(paramerters)
        {
        }

        public int Insert(IFilter valuesFilter)
        {
            string insertInto = string.Format("{0} ({1})",tableName, string.Join(",", columns));
            var commandString = string.Format(InsertCommand, insertInto, valuesFilter.GetFilterString());

            int id = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString))
                {
                    id = (int)command.ExecuteScalar();
                }
            }
            return id;
        }

        //private string GenerateValuesString(object data)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    foreach (var property in data.GetType().GetProperties())
        //    {
        //        builder.Append(property.GetValue(data));
        //    }
        //    return builder.ToString();
        //}

        private string GenerateKeysString(List<object> keys)
        {
            throw new NotImplementedException();
        }
    }
}
