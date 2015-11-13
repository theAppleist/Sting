using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Communicator;
using DAL.CommunicatorImplemenatations;
using StingCore;

namespace Sting.Controllers
{
    public class UsersController : ApiController
    {
        public User Get(int id)
        {

        }

        public void Post(User user)
        {
            //var parameters = new TableCommunicationParameters("dbo.Users", );
            //IInsertCommuncitor communcitor = new InsertCommunicator();
        }
    }
}
