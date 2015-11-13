using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class User : StingAbstractModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }

        

        public User(int userid,int roleId,string firstName, string lastName)
        {
            UserId = userid;
            FirstName = firstName;
            LastName = lastName;
            RoleId = roleId;
        }
    }
}
