using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Communicator
{
    public interface IInsertCommuncitor
    {
        int Insert(object insertedObject);
    }
}
