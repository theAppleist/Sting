using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filter
{
    public class WhereFilter : IFilter
    {
        public IFilter Condition { get; set; }

        public WhereFilter(IFilter condition)
        {
            Condition = condition;
        }

        public string GetFilterString()
        {
            return string.Format("WHERE {0}", Condition);
        }
    }
}
