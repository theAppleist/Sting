using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class SelectFilter : ISelectFilter
    {
        public SelectFilter()
        {

        }

        public string GetFilterString()
        {
            return string.Format("SELECT");
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
