using LocalServicePlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Services>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext dbContext):base(dbContext)
       
        {
            
        }
        public async Task Update(Services service)
        {
         var objFromDb=await _dbContext.Services.FirstOrDefaultAsync(x=>x.Id==service.Id);
            if (objFromDb!=null)
            {
                objFromDb.Name= service.Name;
                objFromDb.Description= service.Description;

                if (objFromDb != null)
                {
                    objFromDb.ServicePic=service.ServicePic;
                }

            }
            _dbContext.Update(objFromDb);

        }
    }
}
