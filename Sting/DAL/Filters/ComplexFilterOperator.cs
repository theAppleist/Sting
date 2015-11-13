using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class ComplexFilterOperator
    {
        public enum Types
        {
            And,
            Or
        }

        public static string ConvertToString(ComplexFilterOperator.Types operation)
        {
            switch (operation)
            {
                case Types.And:
                    return "AND";
                case Types.Or:
                    return "OR";
                default:
                    break;
            }
            return null;
        }
    }
}
