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
using DAL.TableCommunicator;
using StingCore;
using Sting = StingCore.Sting;

namespace Sting.Controllers
{
    public class StingsController : ApiController
    {
        private readonly int LIMIT_STINGS_PER_REQUEST = 20;
        private const string DB_NAME = "dbo.Stings";
        private const string USERS_DB_NAME = "dbo.Users";
        private const string PLACES_DB_NAME = "dbo.Places";

        public IEnumerable<StingCore.Sting> GetStings()
        {
            //var parameters = new TableCommunicationParameters(DB_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            //IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(StingCore.Sting));
            //return (IEnumerable<StingCore.Sting>)communicator.GetRecords(new SelectFilter(), GetValues());
            ITableCrudMethods<StingCore.Sting> crud = new StingsTableCrud();
            return crud.Read(null);
        }

        public StingCore.Sting GetSting(int id)
        {
            /*
            var parameters = new TableCommunicationParameters(DB_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(StingCore.Sting));
            return (StingCore.Sting)communicator.GetRecords(new SelectFilter(),
                GetValues(),
                 new JoinFilter(FilterJoin.Types.InnerJoin, 
                    new ComparisonFilter("dbo.Users.Id","dbo.Stings.UserId",FilterComparer.Types.Equals), new ValueFilter("dbo.Users") ), 
                new JoinFilter(FilterJoin.Types.InnerJoin, 
                    new ComparisonFilter("dbo.Places.Id","dbo.Stings.PlaceId",FilterComparer.Types.Equals), new ValueFilter("dbo.Places") ) ,
                new WhereFilter(new ComparisonFilter("dbo.Stings.Id", ""+id, FilterComparer.Types.Equals))).FirstOrDefault();
            */
            ITableCrudMethods<StingCore.Sting> crud = new StingsTableCrud();
            return crud.Read(id);
        }

        public int PostStings(StingCore.Sting sting)
        {
            ITableCrudMethods<StingCore.Sting> crud = new StingsTableCrud();
            return crud.Insert(sting);
        }
        private int InsertPlace(Place place)
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string> { "Name", "Description", "OwnerId", "Longtitude", "Latitude" });
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);
            SqlPlaceModel sqlModel = new SqlPlaceModel(place);
            return communcitor.Insert(new CombinationFilter(new ValueFilterWithApostrophe(sqlModel.Name), new CombinationFilter(new CombinationFilter(new ValueFilterWithApostrophe(sqlModel.Description), new ValueFilter(sqlModel.OwnerId)), new CombinationFilter(new ValueFilter(sqlModel.Longtitude), new ValueFilter(sqlModel.Latitude)))));
        }

        public void PutStings(int id, [FromBody]StingCore.Sting update)
        {
            throw new NotImplementedException();
        }

        public void DeleteSting(int id)
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


                    id = (int) output.Value;
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
