using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LocalServicePlatform.Domain.ApplicationEnums.ApplicationEnums;

namespace LocalServicePlatform.Domain.ViewModel
{
    public class BookingVM
    {

        public Bookings Bookings { get; set; }
        public List<Bookings> Booking { get; set; }
        public string? searchBox { get; set; } = string.Empty;
        public Guid? ServiceCategoryId { get; set; }
        public Location? Location { get; set; }

        public string PhoneNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceCategoryList { get; set; }

        public IEnumerable<SelectListItem> ServiceTypeList { get; set; }

        public IEnumerable<SelectListItem> WorkTypeList { get; set; }

        public IEnumerable<SelectListItem> LocationList { get; set; }
   
       
    }
}
