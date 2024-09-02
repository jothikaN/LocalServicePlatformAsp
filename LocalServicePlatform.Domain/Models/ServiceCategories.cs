using LocalServicePlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Domain.Models
{
    public class ServiceCategories:BaseModel

    {
      
        [Required]
        public string Name { get; set; }
        //public ICollection<TaskerServiceCategory> TaskerServiceCategories { get; set; } = new List<TaskerServiceCategory>();
    }
}
