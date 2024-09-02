  using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocalServicePlatform.Domain.ViewModel
{
    public class PostVM
    {
        public Post Post { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceCategoryList { get; set; }
        public IEnumerable<SelectListItem> ServiceTypeList { get; set; }

    }
}

