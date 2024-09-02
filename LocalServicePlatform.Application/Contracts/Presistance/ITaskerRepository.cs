using LocalServicePlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LocalServicePlatform.Domain.ApplicationEnums.ApplicationEnums;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
    public interface ITaskerRepository : IGenericRepository<TaskersUpdate>
    {
        Task Update(TaskersUpdate taskersUpdate);
        Task<TaskersUpdate> GetPostById(Guid id);
        Task<TaskersUpdate> GetServiceCategoryById(Guid id);
        Task<List<TaskersUpdate>> GetAllPost();
        Task<List<TaskersUpdate>> GetByUserId(string userId);
        Task<TaskersUpdate> GetByTaskerIdAsync(string taskerId);

        Task<List<TaskersUpdate>> GetAllTaskersUpdates();

    }
}
