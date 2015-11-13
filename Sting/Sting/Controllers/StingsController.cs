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
        private const string DB_NAME = "dbo.Stings";
        public IEnumerable<StingCore.Sting> GetStings()
        {
            var parameters = new TableCommunicationParameters(DB_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (IEnumerable<StingCore.Sting>)communicator.GetRecords(new SelectFilter());
        }

        public StingCore.Sting GetSting(int id)
        {
            var parameters = new TableCommunicationParameters(DB_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (StingCore.Sting)communicator.GetRecords(new SelectFilter(), new WhereFilter(new ComparisonFilter("Id", "1", FilterComparer.Types.Equals)));
        }

        public void PostStings(StingCore.Sting sting)
        {
            var parameters = new TableCommunicationParameters(DB_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string> { });
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);

            var placeId = GetPlace(sting);
            if (placeId == -1)
            {
                placeId = InsertPlace();
            }
            var id = communcitor.Insert(new ValuesFilter(new SqlStindModel(sting.User.UserId, placeId, sting)));
            if (id == -1)
            {
                throw new HttpRequestException("cant add user ");
            }
        }

        private int InsertPlace()
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string> { });
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);
            
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
