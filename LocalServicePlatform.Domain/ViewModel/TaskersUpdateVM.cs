using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LocalServicePlatform.Domain.ApplicationEnums.ApplicationEnums;

namespace LocalServicePlatform.Domain.ViewModel
{
    public class TaskersUpdateVM
    {
        public TaskersUpdate TaskersUpdate { get; set; }
        public List<TaskersUpdate> TaskersUpdates { get; set; }
            public string? searchBox { get; set; } = string.Empty;
            public Guid? ServiceCategoryId { get; set; }
            public Guid? ServiceId { get; set; }

        public Location? Location { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> ServiceCategoryList { get; set; }
            public IEnumerable<SelectListItem> ServiceList { get; set; } = new List<SelectListItem>();
            public IEnumerable<SelectListItem> LocationList { get; set; } = new List<SelectListItem>();
            public List<Guid> SelectedServiceIds { get; set; } = new List<Guid>();


            public List<Services> Services { get; set; } // List of services to display with checkboxes
        }
    } 
