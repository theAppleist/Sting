using DAL.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Communicator
{
    public interface IUpdateCommunicator
    {
        bool Update(IDictionary<string, string> values, IFilter filter);
    }
}
