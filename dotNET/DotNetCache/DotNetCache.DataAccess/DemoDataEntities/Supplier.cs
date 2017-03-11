using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class Supplier
    {
        [Key]
        public int S_SUPPKEY { get; set; }

        public string S_NAME { get; set; }

        public string S_ADDRESS { get; set; }

        public int S_NATIONKEY { get; set; }

        public string S_PHONE { get; set; }

        public decimal S_ACCTBAL { get; set; }

        public string S_COMMENT { get; set; }

    }
}
