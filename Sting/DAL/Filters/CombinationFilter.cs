using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class CombinationFilter : IFilter
    {
        public IFilter[] Filters { get; set; }

        public CombinationFilter(params IFilter[] filters)
        {
            Filters = filters;
        }

        public string GetFilterString()
        {
            return string.Join(",", Filters.ToList());
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
