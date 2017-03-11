using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class Region
    {
        [Key]
        public int R_REGIONKEY { get; set; }

        public string R_NAME { get; set; }

        public string R_COMMENT { get; set; }

    }
}
