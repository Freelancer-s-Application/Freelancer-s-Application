using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Comments
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly FreelancerContext _dbContext;

        public CommentRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentByPostId(int id)
        {
            var comments = await _dbContext.Comments.AsNoTracking()
                .Include(c => c.User)
                .Where(pc => pc.PostId == id)
                .Where(c => c.IsDeleted == false).ToListAsync();

            return comments;
        }

        public async Task UpdateComment(Comment comment)
        {
            //_dbContext.Attach(comment).State = EntityState.Modified;
            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
