using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    [Table("Nation")]
    public class Nation
    {
        [Key]
        public int N_NATIONKEY { get; set; }

        public string N_NAME { get; set; }

        public int N_REGIONKEY { get; set; }

        [ForeignKey("N_REGIONKEY")]
        public virtual Region Region { get; set; }

        public string N_COMMENT { get; set; }

    }
}
