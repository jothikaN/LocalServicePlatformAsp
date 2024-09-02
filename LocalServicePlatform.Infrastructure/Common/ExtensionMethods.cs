using LocalServicePlatform.Domain.Common;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TopSpeed.Infrastructure.Common
{
    public static class ExtensionMethods
    {
        public static async Task<string?> GetCurrentUserId(UserManager<AppUser> _userManager, IHttpContextAccessor _contextAccessor)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                return user?.Id; // Assuming user.Id is of type string
            }
            return userId;
        }



        public static async Task SaveCommonFields(this ApplicationDbContext dbContext, UserManager<AppUser> _userManager, IHttpContextAccessor _contextAccessor)
        {
            var userId = await GetCurrentUserId(_userManager, _contextAccessor);

            IEnumerable<BaseModel> insertEntities = dbContext.ChangeTracker.Entries<BaseModel>()
                .Where(x => x.State == EntityState.Added)
                .Select(x => x.Entity);

            IEnumerable<BaseModel> updateEntities = dbContext.ChangeTracker.Entries<BaseModel>()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity);

            foreach (var item in insertEntities)
            {
                item.CreatedOn = DateTime.UtcNow;
                item.CreatedBy = userId;
                item.ModifiedOn = DateTime.UtcNow;
            }

            foreach (var item in updateEntities)
            {
                item.ModifiedBy = userId;
                item.ModifiedOn = DateTime.UtcNow;
            }

            //await dbContext.SaveChangesAsync(); // Save changes to the database
        }
    }
}
