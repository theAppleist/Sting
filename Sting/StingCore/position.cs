using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StingCore
{
    public class Position : StingAbstractModel
    {
        public double Longtitude { get; set; }
        public double Langtitude { get; set; }

        public Position(double longtitude, double langtitude)
        {
            Longtitude = longtitude;
            Langtitude = langtitude;
        }
    }
}
