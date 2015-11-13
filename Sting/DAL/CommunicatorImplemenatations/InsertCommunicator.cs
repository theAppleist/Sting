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
        private readonly string InsertCommand = "INSERT INTO {0} OUTPUT INSERTED.ID VALUES {1}";  
        
        public InsertCommunicator(TableCommunicationParameters paramerters)
            :base(paramerters)
        {
        }

        public int Insert(object insertedObject, params IFilter[] filters)
        {
            string values = GenerateValuesString(insertedObject);
            string insertInto = string.Format("{0} ({1})",tableName, GenerateColumsString());
            var commandString = string.Format(InsertCommand, insertInto, values);

            int id = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandString);
                id = (int)command.ExecuteScalar();
            }
            return id;
        }

        private string GenerateValuesString(object data)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var property in data.GetType().GetProperties())
            {
                builder.Append(property.GetValue(data));
            }
            return builder.ToString();
        }

        private string GenerateColumsString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var col in columns)
            {
                builder.Append(col);
                builder.Append(",");
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private string GenerateKeysString(List<object> keys)
        {
            throw new NotImplementedException();
        }
    }
}
