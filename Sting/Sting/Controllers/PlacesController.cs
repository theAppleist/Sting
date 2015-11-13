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
    public class PlacesController : ApiController
    {
        public IEnumerable<Place> GetPlaces()
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (IEnumerable<Place>)communicator.GetRecords(new SelectFilter(),null);
        }

        public Place GetPlace(int id)
        {
            var parameters = new TableCommunicationParameters("dbo.Places", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters, typeof(User));
            return (Place)communicator.GetRecords(new SelectFilter(),new WhereFilter(new ComparisonFilter("Id",""+id,FilterComparer.Types.Equals)));
        }


        public void PutPlace(int id, [FromBody]StingCore.Sting update)
        {
            throw new NotImplementedException();
        }

        public void DeletePlace(int id)
        {
            throw new NotImplementedException();
        }
    }
}
