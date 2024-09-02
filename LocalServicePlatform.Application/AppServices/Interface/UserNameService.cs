using LocalServicePlatform.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Services.Interface;

namespace LocalServicePlatform.Application.AppServices.Interface
{
    public class UserNameService : IUserNameService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserNameService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserName(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return String.Empty;
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                return user.UserName;
            }

            return "NA";
        }
    }
}
