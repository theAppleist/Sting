using System;
using System.Collections.Generic;
using System.Configuration;
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
            return (IEnumerable<StingCore.Sting>) communicator.GetRecords(new SelectFilter());
        }

        public StingCore.Sting GetSting(int id)
        {
            var parameters = new TableCommunicationParameters("dbo.Stings", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (StingCore.Sting)communicator.GetRecords(new SelectFilter(),new WhereFilter(new ComparisonFilter("Id","1",FilterComparer.Types.Equals)));
        }

        public void PostStings(StingCore.Sting sting)
        {
            var parameters = new TableCommunicationParameters("dbo.Stings", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>{});
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);
            var userid = 1;
            var placeId = 1;
            var id = communcitor.Insert(new SqlStindModel(userid,placeId,sting));
            if (id == -1)
            {
                throw new HttpRequestException("cant add user ");
            }
        }

        public void PutStings(int id ,[FromBody]StingCore.Sting update)
        {
            throw new NotImplementedException();
        }

        public void DeleteSting(int id)
        {
            throw new NotImplementedException();
        }

        private int SelectId(string tableName,)
    }
}
