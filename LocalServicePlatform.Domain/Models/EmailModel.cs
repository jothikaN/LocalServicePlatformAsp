using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Domain.Models
{
    public class EmailModel

    {
        [Required(ErrorMessage = "Recipient email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string ToAddress { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Body { get; set; }

        // Property for file attachment
        public IFormFile AttachmentFile { get; set; }
    }
}
