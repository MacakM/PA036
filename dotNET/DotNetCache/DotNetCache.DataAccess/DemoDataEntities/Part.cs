using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class Part
    {
        [Key]
        public int P_PARTKEY { get; set; }

        public string P_NAME { get; set; }

        public string P_MFGR { get; set; }

        public string P_BRAND { get; set; }

        public string P_TYPE { get; set; }

        public int P_SIZE { get; set; }

        public string P_CONTAINER { get; set; }

        public decimal P_RETAILPRICE { get; set; }

        public string P_COMMENT { get; set; }

    }
}
