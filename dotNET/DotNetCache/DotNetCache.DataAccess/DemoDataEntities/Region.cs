using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    [Table("Region")]
    public class Region
    {
        [Key]
        public int R_REGIONKEY { get; set; }

        public string R_NAME { get; set; }

        public string R_COMMENT { get; set; }

    }
}
