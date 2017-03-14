using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    /// <summary>
    /// Keys not set in DB!!!
    /// </summary>
    [Table("LineItem")]
    public class LineItem
    {
        [Column(Order = 0), Key]
        public int L_ORDERKEY { get; set; }

        public int L_PARTKEY { get; set; }

        [ForeignKey("L_PARTKEY")]
        public virtual Part Part { get; set; }

        public int L_SUPPKEY { get; set; }

        [ForeignKey("L_SUPPKEY")]
        public virtual Supplier Supplier { get; set; }

        [Column(Order = 1), Key]
        public int L_LINENUMBER { get; set; }

        public decimal L_QUANTITY { get; set; }

        public decimal L_EXTENDEDPRICE { get; set; }

        public decimal L_DISCOUNT { get; set; }

        public decimal L_TAX { get; set; }

        public string L_RETURNFLAG { get; set; }

        public string L_LINESTATUS { get; set; }

        public DateTime L_SHIPDATE { get; set; }

        public DateTime L_COMMITDATE { get; set; }

        public DateTime L_RECEIPTDATE { get; set; }

        public string L_SHIPINSTRUCT { get; set; }

        public string L_SHIPMODE { get; set; }

        public string L_COMMENT { get; set; }

    }

}
