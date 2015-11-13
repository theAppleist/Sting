using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class ValuesFilter : IFilter
    {
        private object _values; 

        public ValuesFilter(object values)
        {
            _values = values;
        }

        public string GetFilterString()
        {
            return String.Format("({0})", string.Join(",", _values));
        }

        private string GetValuesFromObject()
        {
            StringBuilder builder = new StringBuilder();
                foreach (var property in _values.GetType().GetProperties())
                {
                    builder.Append(property.GetValue(_values));
                }
                return builder.ToString();
            
        }
    }
}
