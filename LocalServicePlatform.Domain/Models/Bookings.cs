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
    public class Bookings:BaseModel
    {




        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]


        [Display(Name = "Yor name")]
        public string Name { get; set; }


        [Display(Name = "Select Location")]
        public Location Location { get; set; }



        [Display(Name = "Service")]
        public Guid ServiceId { get; set; }
        [ValidateNever]
        [ForeignKey("ServiceId")] // Specify the foreign key column name
        public Services Services { get; set; }


        [Display(Name = "Phone Number")]
        [Required]
        public float PhoneNumber { get; set; }



        [Display(Name = "Service Category")]
        public Guid ServiceCategoryId { get; set; }
        [ValidateNever]
        [ForeignKey("ServiceCategoryId")] 

        public ServiceCategories ServiceCategories { get; set; }


        public string UserId { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")] 

        public AppUser appUser { get; set; }




        [Display(Name = "Select Service Type")]
        public ServiceType SType { get; set; }



        [Display(Name = "How big is your task")]
        public WorkType WType { get; set; }

    

    }
}
 