using LocalServicePlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Domain.Models
{
    public class PopularPro:BaseModel
    {
       
        [Required]
        public string ProName { get; set; }
        [Required]
        public int PriceDescription { get; set; }
       
        public string ProImage { get; set; }
    }
}
