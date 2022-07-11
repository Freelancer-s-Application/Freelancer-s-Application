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

        public void UpdatePost(Post post)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == post.UserId);
            var major = _dbContext.Majors.FirstOrDefault(u => u.Id == post.MajorId);
            //var author = 
            //var local = _dbContext.Set<Post>()
            //    .Local
            //    .FirstOrDefault(entry => entry.Id == post.Id);
            //if (local != null)
            //{
            //    _dbContext.Entry(local).State = EntityState.Detached;
            //}
            _dbContext.Entry(user).State = EntityState.Unchanged;
            _dbContext.Entry(major).State = EntityState.Unchanged;
            //_dbContext.Entry(post).State = EntityState.Unchanged;
            _dbContext.Posts.Update(post);
        }
    }
}
