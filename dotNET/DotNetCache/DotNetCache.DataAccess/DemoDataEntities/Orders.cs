using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int O_ORDERKEY { get; set; }

        public int O_CUSTKEY { get; set; }

        [ForeignKey("O_CUSTKEY")]
        public virtual Customer Customer { get; set; }

        public string O_ORDERSTATUS { get; set; }

        public decimal O_TOTALPRICE { get; set; }

        public DateTime O_ORDERDATE { get; set; }

        public string O_ORDERPRIORITY { get; set; }

        public string O_CLERK { get; set; }

        public int O_SHIPPRIORITY { get; set; }

        public string O_COMMENT { get; set; }

    }

}
