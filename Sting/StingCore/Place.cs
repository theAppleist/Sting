using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class Place : StingAbstractModel
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public string Description { get; set; }
        public Position Position { get; set; }

        public Place(int placeId, string name, User owner, string description, Position position)
        {
            PlaceId = placeId;
            Name = name;
            Owner = owner;
            Description = description;
            Position = position;
        }   
    }
}
