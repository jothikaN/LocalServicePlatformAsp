using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class BookingsRepository : GenericRepository<Bookings>, IBookingsRepository
    {
        public BookingsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Update(Bookings bookings)
        {
            var objFromDb = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == bookings.Id);
            if (objFromDb != null)
            {
                objFromDb.ServiceId = bookings.Services.Id;
                objFromDb.ServiceCategoryId = bookings.ServiceCategoryId;
                objFromDb.Name = bookings.Name;
                objFromDb.Location = bookings.Location;
                objFromDb.SType = bookings.SType;
                objFromDb.WType = bookings.WType;

                _dbContext.Update(objFromDb);
            }
        }

        public async Task<List<Bookings>> GetAllPost()
        {
            return await _dbContext.Bookings
                .Include(x => x.Services)
                .Include(x => x.ServiceCategories)
                .AsNoTracking() // Ensure asynchronous operations can be performed
                .ToListAsync();
        }

        public async Task<Bookings> GetPostById(Guid id)
        {
            return await _dbContext.Bookings
                .Include(x => x.Services)
                .Include(x => x.ServiceCategories)
                .AsNoTracking() // Ensure asynchronous operations can be performed
                .FirstOrDefaultAsync(x => x.Id == id);
        }

      

        public async Task<List<Bookings>> GetByUserId(string userId)
        {
            try
            {
                return await _dbContext.Bookings

            .Include(b => b.appUser)
            .Include(b => b.Services) // Include services related to each booking
            .Where(b => b.UserId == userId)
            .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return new List<Bookings>();
            }
        }

        public async Task<Bookings> GetServiceById(Guid id)
        {
            return await _dbContext.Bookings
                    .Include(b => b.Services) // Include the services related to the booking
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
