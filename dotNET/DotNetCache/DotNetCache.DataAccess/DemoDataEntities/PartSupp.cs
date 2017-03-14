using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    [Table("PartSupp")]
    public class PartSupp
    {
        [Column(Order = 0), Key]
        public int PS_PARTKEY { get; set; }

        [ForeignKey("PS_PARTKEY")]
        public virtual Part Part { get; set; }

        [Column(Order = 1), Key]
        public int PS_SUPPKEY { get; set; }

        [ForeignKey("PS_SUPPKEY")]
        public virtual Supplier Supplier { get; set; }

        public int PS_AVAILQTY { get; set; }

        public decimal PS_SUPPLYCOST { get; set; }

        public string PS_COMMENT { get; set; }

    }

}
