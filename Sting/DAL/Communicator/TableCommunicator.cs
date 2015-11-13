﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Communicator
{
    public abstract class TableCommunicator
    {
        protected readonly string connectionString;
        protected readonly string tableName;

        public TableCommunicator(TableCommunicationParameters parameters)
        {
            connectionString = parameters.ConnectionString;
            tableName = parameters.TableName;
        }
    }
}
