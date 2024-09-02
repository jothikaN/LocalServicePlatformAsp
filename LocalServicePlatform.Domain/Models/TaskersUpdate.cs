using LocalServicePlatform.Domain.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LocalServicePlatform.Domain.ApplicationEnums.ApplicationEnums;

namespace LocalServicePlatform.Domain.Models
{
    public class TaskersUpdate:BaseModel
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]


        [Display(Name = "Yor name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Select Location")]
        public Location Location { get; set; }



        [Display(Name = "Phone Number")]
        [Required]
        public float PhoneNumber { get; set; }


     



        [Display(Name = "Service")]
        public Guid ServiceId { get; set; }
        [ValidateNever]
        [ForeignKey("ServiceId")] // Specify the foreign key column name

        public Services Services { get; set; }


        public string TaskerId { get; set; }
        [ValidateNever]
        [ForeignKey("TaskerId")] // Specify the foreign key column name

        public AppUser appUser { get; set; }




        [Display(Name = "Upload Image")]
        public string ServiceImage { get; set; }

        public string Email {  get; set; }
    }
}
