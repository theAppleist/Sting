using DAL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Communicator
{
    public interface IReadCommunicator
    {
        Type GetCommunicatorModelType();
        IEnumerable<object> GetRecords(ISelectFilter select, IFilter valuesFilter, params IFilter[] filters);
        IEnumerable<string> GetColumns(); 
    }
}
