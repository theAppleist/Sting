using DAL.Communicator;
using DAL.Filters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CommunicatorImplemenatations
{
    public class DeleteCommunicator : TableCommunicator, IDeleteCommunicator
    {
        public DeleteCommunicator(TableCommunicationParameters parameters)
            :base(parameters)
        {

        }

        public bool Delete(IFilter filter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM {0} {1}", tableName, filter);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                }
                
            }
            return true;
        }
    }
}
