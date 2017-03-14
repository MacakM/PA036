using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    [Table("Supplier")]
    public class Supplier
    {
        [Key]
        public int S_SUPPKEY { get; set; }

        public string S_NAME { get; set; }

        public string S_ADDRESS { get; set; }

        public int S_NATIONKEY { get; set; }
        [ForeignKey("S_NATIONKEY")]
        public virtual Nation Nation { get; set; }

        public string S_PHONE { get; set; }

        public decimal S_ACCTBAL { get; set; }

        public string S_COMMENT { get; set; }

    }
}
