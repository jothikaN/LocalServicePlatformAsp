using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using LocalServicePlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Infrastructure.Common;

namespace LocalServicePlatform.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly UserManager<AppUser> _userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;

        public UnitOfWork(ApplicationDbContext dbContext, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager=userManager;
            _httpContextAccessor = httpContextAccessor;
            Services = new ServiceRepository(_dbContext);
            ServiceCategories = new ServiceCategoryRepository(_dbContext);
            Post=new PostRepository(_dbContext);
            Bookings = new BookingsRepository(_dbContext);
            PopularPro=new PopularProRepository(_dbContext);
            TaskersUpdate=new TaskerRepository(_dbContext);
            TaskersServices=new TaskerServiceRepository(_dbContext);
        }
        public ITaskerServiceRepository TaskersServices { get; private set; }
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);
        public IServiceRepository Services { get; private set; }
        public IServiceCategoryRepository ServiceCategories { get; private set; }
        public IPostRepository Post { get; private set; }
        public IBookingsRepository Bookings { get; private set; }
        public IPopularProRepository PopularPro { get; private set; }
        public ITaskerRepository TaskersUpdate { get; private set; }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
      public async Task SaveAsync()
         {
             await  _dbContext.SaveCommonFields(_userManager, _httpContextAccessor);
             await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Services> GetByIdAsync(Guid serviceId)
        {
            return await _dbContext.Services.FindAsync(serviceId);
        }

    }
}
