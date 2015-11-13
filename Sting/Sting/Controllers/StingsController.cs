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
    public class StingsController : ApiController
    {
        private readonly int LIMIT_STINGS_PER_REQUEST = 20;
        public IEnumerable<StingCore.Sting> GetStings()
        {
            var parameters = new TableCommunicationParameters("dbo.Stings", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (IEnumerable<StingCore.Sting>)communicator.GetRecords(new SelectFilter(), null);
        }

        public StingCore.Sting GetSting(int id)
        {
            var parameters = new TableCommunicationParameters("dbo.Stings", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (StingCore.Sting)communicator.GetRecords(new SelectFilter(), new WhereFilter(new ComparisonFilter("Id", "1", FilterComparer.Types.Equals)));
        }

        public void PostStings(StingCore.Sting sting)
        {
            var parameters = new TableCommunicationParameters("dbo.Stings", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string> { });
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);

            var placeId = GetPlace(sting);
            if (placeId == -1)
            {
                placeId = InsertPlace(sting.Place);
            }
            SqlStingModel model = new SqlStingModel(sting.User.UserId, placeId, sting);
            var id = communcitor.Insert(new CombinationFilter(new CombinationFilter(new ValueFilter(model.PlaceId), new ValueFilter(model.Timestamp)), new CombinationFilter(new ValueFilter(model.Description), new ValueFilter(model.Price))));
            if (id == -1)
            {
                throw new HttpRequestException("cant add user ");
            }
        }

        private int InsertPlace(Place place)
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string> { });
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);
            SqlPlaceModel sqlModel = new SqlPlaceModel(place);
            return communcitor.Insert(new CombinationFilter(new ValueFilter(sqlModel.Name), new CombinationFilter(new CombinationFilter(new ValueFilter(sqlModel.Description), new ValueFilter(sqlModel.OwnerId)), new CombinationFilter(new ValueFilter(sqlModel.Longtitude), new ValueFilter(sqlModel.Latitude)))));
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
                using (SqlCommand command = new SqlCommand("dbo.GetMatchingPlaces", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@lat", sting.Place.Position.Langtitude));
                    command.Parameters.Add(new SqlParameter("@lot", sting.Place.Position.Longtitude));
                    command.Parameters.Add(new SqlParameter("@maxDistance", 1));
                    command.Parameters.Add(new SqlParameter("@name", sting.Place.Name));

                    var reader = command.ExecuteReader();
                    id  = reader.GetInt32(0);
                }
            }
            return id;

        }

    }
}
