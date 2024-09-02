using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class PopularProRepository : GenericRepository<PopularPro>, IPopularProRepository
    {
        public PopularProRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public  async Task Update(PopularPro popularPro)
        {
            var objFromDb = await _dbContext.PopularPros.FirstOrDefaultAsync(x => x.Id == popularPro.Id);
            if (objFromDb != null)
            {
                objFromDb.ProName = popularPro.ProName;
                objFromDb.PriceDescription = popularPro.PriceDescription;

                if (objFromDb != null)
                {
                    objFromDb.ProImage = popularPro.ProImage;
                }

            }
            _dbContext.Update(objFromDb);

        }
    }
    }
    

