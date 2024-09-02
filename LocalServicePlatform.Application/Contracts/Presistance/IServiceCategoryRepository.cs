using LocalServicePlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
  public interface IServiceCategoryRepository:IGenericRepository<ServiceCategories>
    {
        Task Update(ServiceCategories serviceCategories);
        Task<IEnumerable<ServiceCategories>> GetAllServiceCategories();
        Task UpdateServiceCategory(ServiceCategories serviceCategory);
    }
}
