using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Communicator;
using DAL.CommunicatorImplemenatations;
using DAL.Filter;
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
            return (IEnumerable<StingCore.Sting>) communicator.GetRecords();
        }

        public StingCore.Sting GetSting(int id)
        {
            var parameters = new TableCommunicationParameters("dbo.Stings", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (StingCore.Sting)communicator.GetRecords(new WhereFilter(new ComparisonFilter("Id","1",FilterComparer.Types.Equals)));
        }

        public void PostStings(StingCore.Sting sting)
        {
            throw new NotImplementedException();
        }

        public void PutStings(int id ,[FromBody]StingCore.Sting update)
        {
            throw new NotImplementedException();
        }

        public void DeleteSting(int id)
        {
            throw new NotImplementedException();
        }
    }
}
