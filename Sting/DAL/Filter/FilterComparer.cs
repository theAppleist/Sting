using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filter
{
    public class FilterComparer
    {
        public enum Types
        {
            Equals,
            Greater,
            Lesser,
            GreaterAndEquals,
            LesserAndEquals,
            NotEquals
        }
        public static string ConvertToString(FilterComparer.Types comparer)
        {
            switch (comparer)
            {
                case Types.Equals:
                    return "=";
                case Types.Greater:
                    return ">";
                case Types.Lesser:
                    return "<";
                case Types.GreaterAndEquals:
                    return ">=";
                case Types.LesserAndEquals:
                    return "<=";
                case Types.NotEquals:
                    return "!=";
            }
            return null;
        }
    }


    
}
