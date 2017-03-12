using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class PartSupp
    {
        [Key]
        public int PS_PARTKEY { get; set; }

        [ForeignKey("PS_PARTKEY")]
        public Part Part { get; set; }

        [Key]
        public int PS_SUPPKEY { get; set; }

        [ForeignKey("PS_SUPPKEY")]
        public Supplier Supplier { get; set; }

        public int PS_AVAILQTY { get; set; }

        public decimal PS_SUPPLYCOST { get; set; }

        public string PS_COMMENT { get; set; }

    }

}
