using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.PostContents
{
    public class PostContentRepository : Repository<PostContent>, IPostContentRepository
    {
        private readonly FreelancerContext _dbContext;

        public PostContentRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PostContent>> GetAllPostContentByPostId(int id)
        {
            var postcontents = await _dbContext.PostContents
                .Where(pc => pc.PostId == id && !pc.IsDeleted).ToListAsync();

            return postcontents;
        }

        public void UpdatePostContent(PostContent con)
        {
            _dbContext.PostContents.Update(con);
        }
    }
}
