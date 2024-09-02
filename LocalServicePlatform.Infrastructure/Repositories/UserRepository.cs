using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCustomerCountAsync()
        {
            var customerRoleId = (await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer")).Id;
            return await _context.UserRoles.CountAsync(ur => ur.RoleId == customerRoleId);
        }

        public async Task<int> GetTaskerCountAsync()
        {
            var taskerRoleId = (await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Tasker")).Id;
            return await _context.UserRoles.CountAsync(ur => ur.RoleId == taskerRoleId);
        }
        public async Task<int> GetUserCountByRoleIdAsync(string roleId)
        {
            return await _context.UserRoles.CountAsync(ur => ur.RoleId == roleId);
        }







        // Other methods
    }

}
