using LocalServicePlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Application.Contracts.Presistance
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task Update(Post post);
        Task<Post> GetPostById(Guid id);

        Task<List<Post>> GetAllPost();

        Task<List<Post>> GetAllPost(Guid? skipRecord,Guid? serviceId);
        Task<List<Post>> GetAllPost(string? searchName, Guid? serviceId,Guid? serviceTypeId);

    }
}

