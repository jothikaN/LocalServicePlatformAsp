using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class TaskerServiceRepository : GenericRepository<TaskersService>, ITaskerServiceRepository
    {
        public TaskerServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task UpdateAsync(TaskersService taskersService)
        {
            throw new NotImplementedException();
        }
    }
}
