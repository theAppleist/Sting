using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filter
{
    public class Filter : IFilter
    {
        public string Column { get; set; }
        public string Value { get; set; }
        public FilterComparer.Types Comparer { get; set; }

        public Filter(string column, string value, FilterComparer.Types comparer)
        {
            Column = column;
            Value = value;
            Comparer = comparer;
        }

        public string GetFilterString()
        {
            return string.Format("({0}{1}`{2}`)", Column, FilterComparer.ConvertToString(Comparer), Value);
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
