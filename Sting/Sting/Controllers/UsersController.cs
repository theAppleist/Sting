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
    //later fill columns in table parameters
    public class UsersController : ApiController
    {
        public User Get(int id)
        {
            var parameters = new TableCommunicationParameters("dbo.Users", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new List<string>());
            IReadCommunicator communicator = new ReadCommunicator(parameters,typeof(User));
            return (User)communicator.GetRecords(new SelectFilter(),new WhereFilter(new ComparisonFilter("Id", "" + id, FilterComparer.Types.Equals))).FirstOrDefault();
        }

        public void Post(User user)
        {
            var parameters = new TableCommunicationParameters("dbo.Users",ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,new List<string>{"RoleId,FirstName,LasstName"} );
            IInsertCommuncitor communcitor = new InsertCommunicator(parameters);
            var id =communcitor.Insert(new CombinationFilter(new ValueFilter(user.RoleId), new CombinationFilter(new ValueFilter(user.FirstName), new ValueFilter(user.LastName))));
            if (id == -1)
            {
                throw  new HttpRequestException("cant add user ");
            }
        }
    }
}
