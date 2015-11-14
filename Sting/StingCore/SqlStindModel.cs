using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class SqlStingModel
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public SqlStingModel(int userid,int placeId ,Sting sting)
        {
            UserId = userid;
            PlaceId = placeId;
            Timestamp = sting.Timestamp;
            Description = sting.Description;
            Price = (float)sting.Price;
        }
    }
}
