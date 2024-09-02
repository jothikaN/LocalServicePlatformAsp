//using System;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

//namespace LocalServicePlatform.Domain.Models
//{
//    public class TaskerServiceCategory
//    {
//        public Guid TaskersUpdateId { get; set; }

//        [ValidateNever]
//        [ForeignKey("TaskersUpdateId")]
//        public TaskersUpdate TaskersUpdate { get; set; }

//        public Guid ServiceCategoryId { get; set; }

//        [ValidateNever]
//        [ForeignKey("ServiceCategoryId")]
//        public ServiceCategories ServiceCategories { get; set; }
//    }
//}
