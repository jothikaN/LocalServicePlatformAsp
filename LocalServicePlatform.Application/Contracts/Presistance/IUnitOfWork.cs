using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
   public interface IUnitOfWork:IDisposable
    {
        public IServiceRepository Services { get; }
         public IServiceCategoryRepository ServiceCategories { get; }
       public IPostRepository Post { get; }
       public IBookingsRepository Bookings { get; } 
       public IPopularProRepository PopularPro { get; }
        public ITaskerRepository TaskersUpdate { get; }
        public ITaskerServiceRepository TaskersServices { get; }
        public  IUserRepository UserRepository { get; }
          
        Task SaveAsync();
        Task SaveChangesAsync();

    }
}
