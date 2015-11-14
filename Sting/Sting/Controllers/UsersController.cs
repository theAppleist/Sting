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
using DAL.TableCommunicator;
using StingCore;

namespace Sting.Controllers
{
    //later fill columns in table parameters
    public class UsersController : ApiController
    {
        public User Get(int id)
        {
            ITableCrudMethods<User> crud = new UsersTableCommunicator();
            return crud.Read(id);
        }
        
        public int Post(User user)
        {
            ITableCrudMethods<User> crud = new UsersTableCommunicator();
            return crud.Insert(user);
        }
    }
}
