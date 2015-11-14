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
            ITableCrudMethods<StingCore.Sting> crud = new StingsTableCrud();
            var res  = crud.Read(null);
            return res;
        }

        public StingCore.Sting GetSting(int id)
        {
           
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

    }
}
