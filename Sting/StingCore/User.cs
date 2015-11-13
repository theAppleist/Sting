using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }

        

        public User(int userid,string firstName, string lastName, int roleId)
        {
            UserId = userid;
            FirstName = firstName;
            LastName = lastName;
            RoleId = roleId;
        }
    }
}
