﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Filters
{
    public class SetFilter : IFilter
    {
        public IFilter Filter { get; set; }
        public SetFilter(IFilter filter)
        {
            Filter = filter;
        }

        public string GetFilterString()
        {
            return string.Format("SET {0}", Filter);
        }

        public override string ToString()
        {
            return GetFilterString();
        }
    }
}
