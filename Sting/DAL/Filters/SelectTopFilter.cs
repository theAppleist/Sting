using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class SelectTopFilter : ISelectFilter
    {
        public int Number { get; set; }

        public SelectTopFilter(int number)
        {
            Number = number;
        }

        public string GetFilterString()
        {
            return string.Format("SELECT TOP {0}", Number);
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
