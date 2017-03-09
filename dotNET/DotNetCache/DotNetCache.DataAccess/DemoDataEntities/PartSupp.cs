using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class PartSupp
    {
        public int PS_PARTKEY { get; set; }

        public int PS_SUPPKEY { get; set; }

        public int PS_AVAILQTY { get; set; }

        public decimal PS_SUPPLYCOST { get; set; }

        public string PS_COMMENT { get; set; }

    }

}
