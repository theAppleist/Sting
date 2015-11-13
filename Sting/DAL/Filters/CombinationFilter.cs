using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filter
{
    public class CombinationFilter : IFilter
    {
        public IFilter FirstFilter { get; set; }
        public IFilter SecondFilter { get; set; }

        public CombinationFilter(IFilter firstFilter, IFilter secondFilter)
        {
            FirstFilter = firstFilter;
            SecondFilter = secondFilter;
        }

        public string GetFilterString()
        {
            return string.Format("{0},{1}",FirstFilter,SecondFilter);
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
