using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.Filters;

namespace DAL.TableCommunicator
{
    public class StingsTableCrud : ITableCrudMethods<StingCore.Sting>
    {
        private TableCommunicationParameters _parameters;
        //later take from configuration
        private const string TABLE_NAME = "dbo.Stings";
        private const string USERS_TABLE_NAME = "dbo.Users";
        private readonly List<string> COLUMNS = new List<string> { "UserId", "PlaceId", "Timestamp", "Price" };

        public StingsTableCrud()
        {
            
        }
        public StingCore.Sting Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StingCore.Sting> Read(Filters.IFilter filter)
        {
            throw new NotImplementedException();
        }

        public int Insert(StingCore.Sting data)
        {
            throw new NotImplementedException();
        }

        private int GetPlace(StingCore.Sting sting)
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());

            int id = -1;
            using (var conn = new SqlConnection(parameters.ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("dbo.GetMatchingPlaces", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@lat", sting.Place.Position.Langtitude));
                    command.Parameters.Add(new SqlParameter("@lon", sting.Place.Position.Longtitude));
                    command.Parameters.Add(new SqlParameter("@maxDistance", 1));
                    command.Parameters.Add(new SqlParameter("@name", sting.Place.Name));

                    SqlParameter output = new SqlParameter("@id", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    command.Parameters.Add(output);
                    command.ExecuteNonQuery();


                    id = (int)output.Value;
                }
            }
            return id;

        }

        private IFilter GetValues()
        {
            return new CombinationFilter(
                        new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Id")),
                        new CombinationFilter(
                            new CombinationFilter(
                                new CombinationFilter(
                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "Id")),
                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "RoleId"))),
                                new CombinationFilter(
                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "FirstName")),
                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "LastName")))),
                            new CombinationFilter(
                                new CombinationFilter(
                                    new CombinationFilter(
                                        new ValueFilter(string.Format("{0}.{1}", PLACES_DB_NAME, "Id")),
                                        new ValueFilter(string.Format("{0}.{1}", PLACES_DB_NAME, "Name"))),
                                    new CombinationFilter(
                                        new CombinationFilter(
                                            new ValueFilter(string.Format("{0}.{1}", PLACES_DB_NAME, "Description")),
                                            new CombinationFilter(
                                                new CombinationFilter(
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "Id")),
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "RoleId"))),
                                                new CombinationFilter(
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "FirstName")),
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "LastName"))))),
                                        new CombinationFilter(
                                            new ValueFilter(string.Format("{0}.{1}", PLACES_DB_NAME, "Longtitude")),
                                            new ValueFilter(string.Format("{0}.{1}", PLACES_DB_NAME, "Latitude"))))),
                                new CombinationFilter(
                                    new CombinationFilter(
                                        new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Timestamp")),
                                        new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Description"))),
                                    new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Price"))))));
        }
    }
}
