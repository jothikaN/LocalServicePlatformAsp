using LocalServicePlatform.Domain.ApplicationEnums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalServicePlatform.Domain.Common;
using static LocalServicePlatform.Domain.ApplicationEnums.ApplicationEnums;

namespace LocalServicePlatform.Domain.Models
{
    public class Post : BaseModel
    {
        [Required]
        [Display(Name = "Service ")]
        public Guid ServiceId { get; set; }

        [ValidateNever]
        [ForeignKey("ServiceId")]
        public Services Services { get; set; }

        [Display(Name = "Service Category")]
        public Guid ServiceCategoryId { get; set; }

        [ValidateNever]
        [ForeignKey("ServiceCategoryId")]
        public ServiceCategories ServiceCategories { get; set; }

        public string Name { get; set; }
        [Display(Name = " Select Service Type")]

        public ServiceType ServiceType { get; set; }
        [Display(Name = "Service Charge per hour")]
        public float ServiceChargePerHour { get; set; }

        [Display(Name = "Upload Image ")]
        public string ServiceImage { get; set; }



    }
}