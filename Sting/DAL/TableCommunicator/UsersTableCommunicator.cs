using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.CommunicatorImplemenatations;
using DAL.Filters;
using StingCore;

namespace DAL.TableCommunicator
{
    public class UsersTableCommunicator : ITableCrudMethods<User>
    {
        
        private TableCommunicationParameters _parameters;
        //later take from configuration
        private const string TABLE_NAME = "dbo.Users";
        private readonly List<string> COLUMNS = new List<string>{"RoleId","FirstName","LastName"}; 

        public UsersTableCommunicator()
        {
            //need to add id 
            _parameters = new TableCommunicationParameters(TABLE_NAME, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, COLUMNS);

        }

        public User Read(int id)
        {
            IReadCommunicator communicator = new ReadCommunicator(_parameters, typeof(User));
            return (User)communicator.GetRecords(new SelectFilter(), null, new WhereFilter(new ComparisonFilter("Id", "" + id, FilterComparer.Types.Equals))).FirstOrDefault();
        
        }

        public int Insert(User user)
        {
            IInsertCommuncitor communcitor = new InsertCommunicator(_parameters);
            var id = communcitor.Insert(new CombinationFilter(new ValueFilter(user.RoleId), new CombinationFilter(new ValueFilterWithApostrophe(user.FirstName), new ValueFilterWithApostrophe(user.LastName))));
            return id;
        }


        public User[] Read(IFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
