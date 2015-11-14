using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Filters;
using StingCore;

namespace DAL.TableCommunicator
{
    public interface ITableCrudMethods<T>
    {
        T Read(int id);
        IEnumerable<T> Read(IFilter filter); 
        int Insert(T data);
    }
}
