using LocalServicePlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
    public interface IPopularProRepository:IGenericRepository<PopularPro>
    {
        Task Update(PopularPro popularPro);
    }
}
