using DAL.Filter;
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
        IEnumerable<object> GetRecords(params IFilter[] filters);
        IEnumerable<string> GetColumns(); 
    }
}
