using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Domain.ViewModel
{
    public class HomePostVM
    {
        public List<Post> Posts { get; set; }
        public List<PopularPro> PopularPros { get; set; }

        public string? searchBox { get; set; }=string.Empty;
        public Guid? ServiceId { get; set; }

        public Guid? ServiceCategoryId { get; set; }



        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceCategoryList { get; set; }
    }
}
