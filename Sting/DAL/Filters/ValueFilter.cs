using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class ValueFilter : IFilter
    {
        public object Value { get; set; }
        public ValueFilter(object value)
        {
            Value = value;
        }

        public virtual string GetFilterString()
        {
            return Value.ToString();
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
