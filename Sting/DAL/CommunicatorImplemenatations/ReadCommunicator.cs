using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;
using DAL.Filter;

namespace DAL.CommunicatorImplemenatations
{
    public class ReadCommunicator : TableCommunicator, IReadCommunicator
    {
        public ReadCommunicator(TableCommunicationParameters parameters)
            :base(parameters)
        {

        }

        public IEnumerable<string> GetColumns()
        {
            
        }

        public Type GetCommunicatorModelType()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetRecords(IFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
