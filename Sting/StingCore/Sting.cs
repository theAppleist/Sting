using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    [Serializable]
    public class Sting : StingAbstractModel
    {
        public int StingId { get; set; }
        public User User { get; set; }
        public Place Place { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }  

        public Sting(int stingId, User user, Place place, DateTime timestamp, string description, double price)
        {
            StingId = stingId;
            User = user;
            Place = place;
            Timestamp = timestamp;
            Description = description;
            Price = price;
        }
    }
}
