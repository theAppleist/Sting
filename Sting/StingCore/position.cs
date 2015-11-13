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
        public float Longtitude { get; private set; }
        public float Langtitude { get; private set; }

        public Position(float longtitude, float langtitude)
        {
            Longtitude = longtitude;
            Langtitude = langtitude;
        }
    }
}
