using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
    public interface IRoleRepository
    {
        Task<string> GetRoleIdByNameAsync(string roleName);
    }
}
