using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class FilterJoin
    {
        public enum Types
        {
            InnerJoin,
            LeftJoin,
            RightJoin,
            FullJoin 
        }

        public static string ConvertToString(FilterJoin.Types join)
        {
            switch (join)
            {
                case Types.InnerJoin:
                    return "INNER JOIN";
                case Types.LeftJoin:
                    return "LEFT JOIN";
                case Types.RightJoin:
                    return "RIGHT JOIN";
                case Types.FullJoin:
                    return "FULL JOIN";
                default:
                    break;
            }
            return null;
        }
    }
}
