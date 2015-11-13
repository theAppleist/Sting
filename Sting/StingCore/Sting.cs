using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class Sting
    {
        public int StingId { get; set; }
        public User UserId { get; set; }
        public Place PlaceId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public Sting(int stingId, User userId, Place placeId, DateTime timestamp, string description, int price)
        {
            StingId = stingId;
            UserId = userId;
            PlaceId = placeId;
            Timestamp = timestamp;
            Description = description;
            Price = price;
        }
    }
}
