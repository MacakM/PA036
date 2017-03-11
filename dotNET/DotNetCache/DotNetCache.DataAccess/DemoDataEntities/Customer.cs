﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class Customer
    {
        public int C_CUSTKEY { get; set; }

        public string C_NAME { get; set; }

        public string C_ADDRESS { get; set; }

        public int C_NATIONKEY { get; set; }

        public string C_PHONE { get; set; }

        public decimal C_ACCTBAL { get; set; }

        public string C_MKTSEGMENT { get; set; }

        public string C_COMMENT { get; set; }

    }
}