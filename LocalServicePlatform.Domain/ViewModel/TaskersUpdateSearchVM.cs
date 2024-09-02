using System.Collections.Generic;
using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocalServicePlatform.Domain.ViewModel
{
    public class TaskersUpdateSearchVM
    {
        public string SearchDescription { get; set; }
        public string SearchLocation { get; set; }
        public IEnumerable<TaskersUpdate> TaskersUpdates { get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; }
    }
}
