using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Communicator;
using DAL.CommunicatorImplemenatations;
using DAL.Filters;
using StingCore;

namespace Sting.Controllers
{
    public class PlacesController : ApiController
    {
        private const string DB_NAME = "dbo.Places";
        private const string USERS_DB_NAME = "dbo.Users";

        public IEnumerable<Place> GetPlaces()
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(Place));
            return (IEnumerable<Place>)communicator.GetRecords(new SelectFilter(), GetValues());
        }

        public Place GetPlace(int id)
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(Place));
            return (Place)communicator.GetRecords(new SelectFilter(), GetValues(),
                new JoinFilter(FilterJoin.Types.InnerJoin, 
                    new ComparisonFilter("dbo.Users.Id","dbo.Places.OwnerId",FilterComparer.Types.Equals), new ValueFilter("dbo.Users") ) 
               ,new WhereFilter(new ComparisonFilter("dbo.Places.Id",""+id,FilterComparer.Types.Equals))).FirstOrDefault();
        }

        public int PostPlace(Place place)
        {
            var id = GetPlace(place);
            if (id == -1)
            {
                return InsertPlace(place);
            }
            return id;
        }
        public void PutPlace(int id, [FromBody]StingCore.Sting update)
        {
            throw new NotImplementedException();
        }

        public void DeletePlace(int id)
        {
            throw new NotImplementedException();
        }

        private IFilter GetValues()
        {
            return new CombinationFilter(
                new CombinationFilter( 
                    new CombinationFilter(
                        new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Id")),
                        new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Name"))),
                    new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Description"))),
                new CombinationFilter(
                    new CombinationFilter(
                        new CombinationFilter(
                            new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "Id")),
                            new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "RoleId"))),
                        new CombinationFilter(
                            new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "FirstName")),
                            new ValueFilter(string.Format("{0}.{1}", USERS_DB_NAME, "LastName")))),
                    new CombinationFilter(
                        new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Longtitude")),
                        new ValueFilter(string.Format("{0}.{1}", DB_NAME, "Latitude")))
                    )
                );
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
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string> {"Name","Description","OwnerId","Longtitude","Latitude" });
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);
            SqlPlaceModel sqlModel = new SqlPlaceModel(place);
            return communcitor.Insert(new CombinationFilter(new ValueFilterWithComma(sqlModel.Name), new CombinationFilter(new CombinationFilter(new ValueFilterWithComma(sqlModel.Description), new ValueFilter(sqlModel.OwnerId)), new CombinationFilter(new ValueFilter(sqlModel.Longtitude), new ValueFilter(sqlModel.Latitude)))));
        }


    }

}
