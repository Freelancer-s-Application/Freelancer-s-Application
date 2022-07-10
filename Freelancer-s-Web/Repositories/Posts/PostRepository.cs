using Freelancer_s_Web.Commons;
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

        public async Task<Post> GetPost(int? id)
        {
            var post = await _dbContext.Posts
                .Include(p => p.Major)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);            
            return post;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            List<Post> posts = new List<Post>();

            posts = await _dbContext.Posts
                .Include(p => p.Major)
                .Include(p => p.User).ToListAsync();

            return posts;
        }

        public async Task CreatePost(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _dbContext.Attach(post).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
