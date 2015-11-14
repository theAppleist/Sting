using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class ValueFilterWithComma : ValueFilter
    {
        public ValueFilterWithComma(object data)
            : base(data)
        {
            
        }

        public override string GetFilterString()
        {
            return String.Format("'{0}'",base.GetFilterString());
        }
    }
}
