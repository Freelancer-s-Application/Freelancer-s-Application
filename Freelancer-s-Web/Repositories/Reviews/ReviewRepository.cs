using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Reviews
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly FreelancerContext _dbContext;

        public ReviewRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
