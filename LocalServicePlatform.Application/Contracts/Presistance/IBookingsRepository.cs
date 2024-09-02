using LocalServicePlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
    public interface IBookingsRepository:IGenericRepository<Bookings>
    {
        Task Update (Bookings bookings);
        Task<Bookings> GetPostById(Guid id);
        Task<Bookings> GetServiceById(Guid id);
        Task<List<Bookings>> GetAllPost();
        Task<List<Bookings>> GetByUserId(string userId);
    }
}
