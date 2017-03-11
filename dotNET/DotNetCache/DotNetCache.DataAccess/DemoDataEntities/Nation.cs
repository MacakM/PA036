using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.DataAccess.DemoDataEntities
{
    public class Nation
    {
        [Key]
        public int N_NATIONKEY { get; set; }

        public string N_NAME { get; set; }

        public int N_REGIONKEY { get; set; }

        public string N_COMMENT { get; set; }

    }
}
