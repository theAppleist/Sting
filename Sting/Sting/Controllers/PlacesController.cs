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
            return (Place)communicator.GetRecords(new SelectFilter(), GetValues(),new JoinFilter(FilterJoin.Types.InnerJoin, 
                new ComparisonFilter("dbo.Users.Id","dbo.Places.OwnerId",FilterComparer.Types.Equals), new ValueFilter("dbo.Users") ) 
                ,new WhereFilter(new ComparisonFilter("dbo.Places.Id",""+id,FilterComparer.Types.Equals))).FirstOrDefault();
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
    }
}
