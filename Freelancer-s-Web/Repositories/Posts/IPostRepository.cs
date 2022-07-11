using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Posts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPost(int? id);

        Task<List<Post>> GetAllPosts();

        Task CreatePost(Post post);

        void UpdatePost(Post post);
    }
}
