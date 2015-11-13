using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StingCore;

namespace Sting.Controllers
{
    public class PlacesController : ApiController
    {
        public IEnumerable<Place> GetPlaces()
        {
            throw new NotImplementedException();
        }

        public Place GetPlace(int id)
        {
            throw new NotImplementedException();    
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
