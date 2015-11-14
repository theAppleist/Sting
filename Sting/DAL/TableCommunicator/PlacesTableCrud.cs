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
    public class PlacesTableCrud : ITableCrudMethods<Place>
    {
        private TableCommunicationParameters _parameters;
        //later take from configuration
        private const string TABLE_NAME = "dbo.Places";
        private const string USERS_TABLE_NAME = "dbo.Users";
        private readonly List<string> COLUMNS = new List<string> { "Name", "Description", "OwnerId","Longtitude","Latitude" };

        public PlacesTableCrud()
        {
            _parameters = new TableCommunicationParameters(TABLE_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,COLUMNS);
        }
        public Place Read(int id)
        {
            IReadCommunicator communicator = new ReadCommunicator(_parameters, typeof(Place));
            return (Place)communicator.GetRecords(new SelectFilter(), GetValues(),
                new JoinFilter(FilterJoin.Types.InnerJoin,
                    new ComparisonFilter("dbo.Users.Id", "dbo.Places.OwnerId", FilterComparer.Types.Equals), new ValueFilter("dbo.Users"))
               , new WhereFilter(new ComparisonFilter("dbo.Places.Id", id.ToString(), FilterComparer.Types.Equals))).FirstOrDefault();
        }

        public int Insert(Place place)
        {
            var id = GetPlace(place);
            if (id == -1)
            {
                return InsertPlace(place);
            }
            return id;
        }


        public Place[] Read(Filters.IFilter filter)
        {
            IReadCommunicator communicator = new ReadCommunicator(_parameters, typeof(Place));
            var result = communicator.GetRecords(new SelectFilter(), GetValues(), 
                    new JoinFilter(FilterJoin.Types.InnerJoin,
                    new ComparisonFilter("dbo.Users.Id", "dbo.Places.OwnerId", FilterComparer.Types.Equals), new ValueFilter("dbo.Users"))
            );
            return result.OfType<Place>().ToArray();
        }
        private IFilter GetValues()
        {
            return new CombinationFilter(
                new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Id")),
                new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Name")),
                new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Description")),
                new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "Id")),
                new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "RoleId")),
                new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "FirstName")),
                new ValueFilter(string.Format("{0}.{1}", USERS_TABLE_NAME, "LastName")),
                new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Longtitude")),
                new ValueFilter(string.Format("{0}.{1}", TABLE_NAME, "Latitude")));
        }
        private int GetPlace(Place place)
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());

            int id = -1;
            using (var conn = new SqlConnection(parameters.ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("dbo.GetMatchingPlaces", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@lat", place.Position.Langtitude));
                    command.Parameters.Add(new SqlParameter("@lon", place.Position.Longtitude));
                    command.Parameters.Add(new SqlParameter("@maxDistance", 1));
                    command.Parameters.Add(new SqlParameter("@name", place.Name));

                    SqlParameter output = new SqlParameter("@id", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    command.Parameters.Add(output);
                    command.ExecuteNonQuery();


                    id = (int)output.Value;
                }
            }
            return id;

        }

        private int InsertPlace(Place place)
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string> { "Name", "Description", "OwnerId", "Longtitude", "Latitude" });
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);
            SqlPlaceModel sqlModel = new SqlPlaceModel(place);
            return communcitor.Insert(new CombinationFilter(new ValueFilterWithApostrophe(sqlModel.Name), new CombinationFilter(new CombinationFilter(new ValueFilterWithApostrophe(sqlModel.Description), new ValueFilter(sqlModel.OwnerId)), new CombinationFilter(new ValueFilter(sqlModel.Longtitude), new ValueFilter(sqlModel.Latitude)))));
        }

    }
}
