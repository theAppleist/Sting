using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StingCore;

namespace Sting.Controllers
{
    public class StingsController : ApiController
    {
        public IEnumerable<StingCore.Sting> GetStings()
        {
            throw new NotImplementedException();
        }

        public StingCore.Sting GetSting(int id)
        {
            throw new NotImplementedException();
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
