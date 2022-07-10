using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ApplicationForms
{
    public class ApplicationFormRepository : Repository<ApplicationForm>, IApplicationFormRepository
    {
        private readonly FreelancerContext _dbContext;

        public ApplicationFormRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ApplicationForm> GetAllFormByPostIdExceptCV(int postId)
        {
            return _dbContext.ApplicationForms
                .Include(f => f.Post)
                .Include(f => f.User)
                .Where(f => f.PostId == postId)
                .Select(f => new ApplicationForm()
                {
                    Id = f.Id,
                    User = f.User,
                    Post = f.Post,
                });
        }
    }
}
