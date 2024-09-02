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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }



        public async Task Update(Post post)
        {
            var objFromDb = await _dbContext.Post.FirstOrDefaultAsync(x => x.Id == post.Id);
            if (objFromDb != null)
            {
                objFromDb.ServiceId = post.ServiceId;
                objFromDb.ServiceCategoryId = post.ServiceCategoryId;
                objFromDb.Name = post.Name;
                objFromDb.ServiceType = post.ServiceType;
                objFromDb.ServiceChargePerHour = post.ServiceChargePerHour;

                if (post.ServiceImage != null)
                {
                    objFromDb.ServiceImage = post.ServiceImage;
                }
                _dbContext.Update(objFromDb);
            }
        }
        public async Task<List<Post>> GetAllPost()
        {
            return await _dbContext.Post.Include(x => x.Services).Include(x => x.ServiceCategories).ToListAsync();
        }

        public async Task<Post> GetPostById(Guid id)
        {
            return await _dbContext.Post.Include(x => x.Services).Include(x => x.ServiceCategories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Post>> GetAllPost(Guid? skipRecord, Guid? serviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetAllPost(string searchName, Guid? serviceId, Guid? serviceCategoryId)
        {
            var query = _dbContext.Post.Include(x => x.Services).Include(x => x.ServiceCategories).OrderByDescending(x => x.ModifiedOn);

            if(searchName==string.Empty&& serviceId==Guid.Empty&& serviceCategoryId == Guid.Empty)
            {
                return await query.ToListAsync();
            }
            if (serviceId != Guid.Empty) 
            { 
                query= (IOrderedQueryable<Post>)query.Where(x=>x.ServiceId==serviceId);
            }
            if (serviceCategoryId != Guid.Empty)
            {
                query = (IOrderedQueryable<Post>)query.Where(x => x.ServiceCategoryId== serviceCategoryId);
            }
            
            if(!string.IsNullOrEmpty(searchName))
            {
                query = (IOrderedQueryable<Post>)query.Where(x=>x.Name.Contains(searchName));
            }

            return await query.ToListAsync();
        }
    }

}

