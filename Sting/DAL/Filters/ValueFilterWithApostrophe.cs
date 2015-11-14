using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class ValueFilterWithApostrophe : ValueFilter
    {
        public ValueFilterWithApostrophe(object data)
            : base(data)
        {
            
        }

        public override string GetFilterString()
        {
            return string.Format("'{0}'",base.GetFilterString());
        }
    }
}
