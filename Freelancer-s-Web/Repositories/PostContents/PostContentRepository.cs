using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}
