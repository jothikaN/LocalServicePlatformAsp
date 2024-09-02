using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class ServiceCategoryRepository : GenericRepository<ServiceCategories>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            
        }
        public async Task<IEnumerable<ServiceCategories>> GetAllServiceCategories()
        {
            return await _dbContext.ServiceCategories.ToListAsync();
        }

        public async Task Update(ServiceCategories serviceCategories)
        {
            var existingCategory = await _dbContext.ServiceCategories.FindAsync(serviceCategories.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = serviceCategories.Name;
                _dbContext.ServiceCategories.Update(existingCategory);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateServiceCategory(ServiceCategories serviceCategory)
        {
            var existingCategory = await _dbContext.ServiceCategories.FindAsync(serviceCategory.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = serviceCategory.Name;
                _dbContext.ServiceCategories.Update(existingCategory);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
