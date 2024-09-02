using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.ApplicationEnums;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;  // Use this for EF Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class TaskerRepository : GenericRepository<TaskersUpdate>, ITaskerRepository
    {
        public TaskerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TaskersUpdate>> GetAllPost()
        {
            return await _dbContext.TaskersUpdate
                .Include(b => b.Services)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TaskersUpdate>> GetAllTaskersUpdates()
        {
            return await _dbContext.TaskersUpdate
                
                .Include(x => x.Services)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskersUpdate> GetByTaskerIdAsync(string taskerId)
        {
            return await _dbContext.TaskersUpdate
                .FirstOrDefaultAsync(tu => tu.TaskerId == taskerId);
        }

        public async Task<List<TaskersUpdate>> GetByUserId(string userId)
        {
            try
            {
                return await _dbContext.TaskersUpdate
                    .Include(b => b.appUser)
                    .Include(b => b.Services)
                    .Where(b => b.TaskerId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return new List<TaskersUpdate>();
            }
        }

        public async Task<TaskersUpdate> GetPostById(Guid id)
        {
            return await _dbContext.TaskersUpdate
                .Include(b => b.Services)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TaskersUpdate> GetServiceCategoryById(Guid id)
        {
            return await _dbContext.TaskersUpdate
                .Include(b => b.Services)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Update(TaskersUpdate taskersUpdate)
        {
            var objFromDb = await _dbContext.TaskersUpdate
                .FirstOrDefaultAsync(x => x.Id == taskersUpdate.Id);

            if (objFromDb != null)
            {
                objFromDb.ServiceId = taskersUpdate.ServiceId;
                objFromDb.Name = taskersUpdate.Name;
                objFromDb.Location = taskersUpdate.Location;
                objFromDb.Description = taskersUpdate.Description;
                objFromDb.PhoneNumber = taskersUpdate.PhoneNumber;
                objFromDb.Services = taskersUpdate.Services;

                if (taskersUpdate.ServiceImage != null)
                {
                    objFromDb.ServiceImage = taskersUpdate.ServiceImage;
                }

                _dbContext.Update(objFromDb);
                await _dbContext.SaveChangesAsync(); // Ensure changes are saved
            }
        }
    }
}
