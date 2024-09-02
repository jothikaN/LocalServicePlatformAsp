using LocalServicePlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Domain.Models
{
    public class Services:BaseModel
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Service Image")]
        public string ServicePic { get; set; }

        public ICollection<TaskersService> TaskersServices { get; set; }

    }
}
