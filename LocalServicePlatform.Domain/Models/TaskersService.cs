using LocalServicePlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Domain.Models
{
    public class TaskersService:BaseModel
    {
        public Guid TaskersUpdateId { get; set; }
        public TaskersUpdate TaskersUpdate { get; set; }

        public Guid ServiceId { get; set; }
        public Services Services { get; set; }
    }
}
