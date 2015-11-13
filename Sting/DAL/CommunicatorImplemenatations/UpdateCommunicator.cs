using DAL.Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Filter;
using System.Data.SqlClient;

namespace DAL.CommunicatorImplemenatations
{
    public class UpdateCommunicator : TableCommunicator, IUpdateCommunicator
    {
        public UpdateCommunicator(TableCommunicationParameters parameters)
            :base(parameters)
        {

        }

        public bool Update(IDictionary<string, string> values, params IFilter[] filters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    string baseQuery = string.Format("UPDATE {0}");
                    foreach (IFilter filter in filters)
                    {
                        baseQuery += string.Format(" {0}", filter.GetFilterString());
                    }
                    command.CommandText = baseQuery;

                    using(SqlDataReader reader = command.ExecuteReader())
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
