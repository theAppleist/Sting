using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Communicator;

namespace DAL.CommunicatorImplemenatations
{
    public class InsertCommunicator :IInsertCommuncitor
    {
        private readonly string InsertCommand = "INSERT INTO {0} OUTPUT INSERTED.ID VALUES {1}";  
        private TableCommunicationParameters _parameters;

        public InsertCommunicator(TableCommunicationParameters paramerters)
        {
            _parameters = paramerters;
        }

        public int Insert(object insertedObject)
        {
           
        }

        private string GenerateValuesString(PropertyInfo [] properties)
        {
            
        }

        private string GenerateKeysString(List<object> keys)
        {
            throw new NotImplementedException();
        }
    }
}
