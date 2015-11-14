using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class ComparisonFilter : IFilter
    {
        public string LeftSide { get; set; }
        public string RightSide { get; set; }
        public FilterComparer.Types Comparer { get; set; }

        public ComparisonFilter(string leftSide, string rightSide, FilterComparer.Types comparer)
        {
            LeftSide = leftSide;
            RightSide = rightSide;
            Comparer = comparer;
        }

        public string GetFilterString()
        {
            return string.Format("({0}{1}{2})", LeftSide, FilterComparer.ConvertToString(Comparer), RightSide);
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
