using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class ComplexFilter : IFilter
    {
        public IFilter FirstFilter { get; set; }
        public IFilter SecondFilter { get; set; }
        public ComplexFilterOperator.Types Operation { get; set; }

        public ComplexFilter(IFilter firstFilter, IFilter secondFilter, ComplexFilterOperator.Types operation)
        {
            FirstFilter = firstFilter;
            SecondFilter = secondFilter;
            Operation = operation;
        }
        public string GetFilterString()
        {
            return string.Format("({0} {1} {2})", FirstFilter.GetFilterString(), ComplexFilterOperator.ConvertToString(Operation), SecondFilter.GetFilterString());
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
