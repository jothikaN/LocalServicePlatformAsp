using LocalServicePlatform.Domain.Models;
using System;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
    public interface ITaskerServiceRepository : IGenericRepository<TaskersService>
    {
        // Add methods specific to TaskersService if needed
        Task UpdateAsync(TaskersService taskersService);
    }
}
