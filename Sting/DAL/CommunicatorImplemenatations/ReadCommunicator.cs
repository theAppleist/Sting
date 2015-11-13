using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;

namespace DAL.CommunicatorImplemenatations
{
    public class ReadCommunicator : TableCommunicator
    {
        public ReadCommunicator(TableCommunicationParameters parameters)
            :base(parameters)
        {

        }
    }
}
