using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class Orders
    {
        [Key]
        public int O_ORDERKEY { get; set; }

        public int O_CUSTKEY { get; set; }

        public string O_ORDERSTATUS { get; set; }

        public decimal O_TOTALPRICE { get; set; }

        public DateTime O_ORDERDATE { get; set; }

        public string O_ORDERPRIORITY { get; set; }

        public string O_CLERK { get; set; }

        public int O_SHIPPRIORITY { get; set; }

        public string O_COMMENT { get; set; }

    }

}
