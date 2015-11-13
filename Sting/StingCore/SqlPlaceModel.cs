using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class SqlPlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public float Longtitude { get; set; }
        public float Latitude { get; set; }

        public SqlPlaceModel(Place place)
        {
            Id = place.PlaceId;
            Name = place.Name;
            Description = place.Description;
            OwnerId = place.Owner.UserId;
            Longtitude = place.Position.Longtitude;
            Latitude = place.Position.Langtitude;

        }
    }
}
