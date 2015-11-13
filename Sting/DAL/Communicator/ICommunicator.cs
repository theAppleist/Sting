using DAL.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Communicator
{
    public interface ICommunicator
    {
        Type GetCommunicatorModelType();
        IEnumerable<object> GetRecords(IFilter filter);
        IEnumerable<string> GetColumns(); 
    }
}
