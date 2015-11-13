using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class JoinFilter : IFilter
    {
        public FilterJoin.Types Type { get; set; }
        public IFilter ComparisonFilter { get; set; }
        public IFilter TableName { get; set; }

        public JoinFilter(FilterJoin.Types type, IFilter comparisonFilter, IFilter tableName)
        {
            Type = type;
            ComparisonFilter = comparisonFilter;
            TableName = tableName;
        }
        public string GetFilterString()
        {
            return string.Format("{0} {1} ON {2}", FilterJoin.ConvertToString(Type), TableName, ComparisonFilter);
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
