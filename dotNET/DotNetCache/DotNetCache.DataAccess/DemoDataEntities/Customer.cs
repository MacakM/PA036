using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class Customer
    {
        [Key]
        public int C_CUSTKEY { get; set; }

        public string C_NAME { get; set; }

        public string C_ADDRESS { get; set; }

        public int C_NATIONKEY { get; set; }

        [ForeignKey("C_NATIONKEY")]
        public Nation Nation { get; set; }

        public string C_PHONE { get; set; }

        public decimal C_ACCTBAL { get; set; }

        public string C_MKTSEGMENT { get; set; }

        public string C_COMMENT { get; set; }

    }
}
