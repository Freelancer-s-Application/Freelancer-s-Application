using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Posts
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly FreelancerContext _dbContext;

        public PostRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _dbContext.Posts
                .Include(p => p.Major)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }
    }
}
