using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class Place
    {
        public int PlaceId { get; set; }
        public Position Position { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }

        public Place(int placeId, Position position, string name, string description, User owner)
        {
            PlaceId = placeId;
            Position = position;
            Name = name;
            Description = description;
            Owner = owner;
        }
    }
}
