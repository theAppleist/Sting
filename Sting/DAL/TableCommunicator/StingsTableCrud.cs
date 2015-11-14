using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.CommunicatorImplemenatations;
using DAL.Filters;
using StingCore;

namespace DAL.TableCommunicator
{
    public class StingsTableCrud : ITableCrudMethods<StingCore.Sting>
    {
        private TableCommunicationParameters _parameters;
        //later take from configuration
        private const string TABLE_NAME = "dbo.Stings";
        private const string USERS_TABLE_NAME = "dbo.Users";
        private const string PLACES_TABLE_NAME = "dbo.Places";

        private readonly List<string> COLUMNS = new List<string> { "UserId", "PlaceId", "Timestamp","Description", "Price" };

        public StingsTableCrud()
        {
            _parameters = new TableCommunicationParameters(TABLE_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, COLUMNS);
        }
        public StingCore.Sting Read(int id)
        {
            IReadCommunicator communicator = new ReadCommunicator(_parameters, typeof(StingCore.Sting));
            return (StingCore.Sting)communicator.GetRecords(new SelectFilter(),
                GetValues(),
                 new JoinFilter(FilterJoin.Types.InnerJoin,
                    new ComparisonFilter("dbo.Users.Id", "dbo.Stings.UserId", FilterComparer.Types.Equals), new ValueFilter(USERS_TABLE_NAME)),
                new JoinFilter(FilterJoin.Types.InnerJoin,
                    new ComparisonFilter("dbo.Places.Id", "dbo.Stings.PlaceId", FilterComparer.Types.Equals), new ValueFilter(PLACES_TABLE_NAME)),
                new WhereFilter(new ComparisonFilter("dbo.Stings.Id", id.ToString(), FilterComparer.Types.Equals))).FirstOrDefault();
        }

        public StingCore.Sting[] Read(Filters.IFilter filter)
        {
            IReadCommunicator communicator = new ReadCommunicator(_parameters, typeof(StingCore.Sting));
            var result = communicator.GetRecords(new SelectFilter(), GetValues(), new JoinFilter(FilterJoin.Types.InnerJoin,
                    new ComparisonFilter("dbo.Users.Id", "dbo.Stings.UserId", FilterComparer.Types.Equals), new ValueFilter(USERS_TABLE_NAME)),
                new JoinFilter(FilterJoin.Types.InnerJoin,
                    new ComparisonFilter("dbo.Places.Id", "dbo.Stings.PlaceId", FilterComparer.Types.Equals), new ValueFilter(PLACES_TABLE_NAME)));
            return result.OfType<StingCore.Sting>().ToArray();
        }

        public int Insert(StingCore.Sting sting)
        {
            IInsertCommuncitor communcitor = new InsertCommunicator(_parameters);

            var placeId = GetPlace(sting);
            if (placeId == -1)
            {
                ITableCrudMethods<Place> place = new PlacesTableCrud();
                placeId = place.Insert(sting.Place);
            }
            SqlStingModel model = new SqlStingModel(sting.User.UserId, placeId, sting);
            var id = communcitor.Insert(new CombinationFilter(new ValueFilter(sting.User.UserId), new CombinationFilter(new CombinationFilter(new ValueFilter(model.PlaceId), new ValueFilterWithApostrophe(model.Timestamp)), new CombinationFilter(new ValueFilterWithApostrophe(model.Description), new ValueFilter(model.Price)))));
            return id;
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
                        new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Id")),
                        new CombinationFilter(
                            new CombinationFilter(
                                new CombinationFilter(
                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "Id")),
                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "RoleId"))),
                                new CombinationFilter(
                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "FirstName")),
                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "LastName")))),
                            new CombinationFilter(
                                new CombinationFilter(
                                    new CombinationFilter(
                                        new ValueFilter(string.Format("{0}.{1}", PLACES_TABLE_NAME, "Id")),
                                        new ValueFilter(string.Format("{0}.{1}", PLACES_TABLE_NAME, "Name"))),
                                    new CombinationFilter(
                                        new CombinationFilter(
                                            new ValueFilter(string.Format("{0}.{1}", PLACES_TABLE_NAME, "Description")),
                                            new CombinationFilter(
                                                new CombinationFilter(
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "Id")),
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "RoleId"))),
                                                new CombinationFilter(
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "FirstName")),
                                                    new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "LastName"))))),
                                        new CombinationFilter(
                                            new ValueFilter(string.Format("{0}.{1}", PLACES_TABLE_NAME, "Longtitude")),
                                            new ValueFilter(string.Format("{0}.{1}", PLACES_TABLE_NAME, "Latitude"))))),
                                new CombinationFilter(
                                    new CombinationFilter(
                                        new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Timestamp")),
                                        new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Description"))),
                                    new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Price"))))));
        }
    }
}
