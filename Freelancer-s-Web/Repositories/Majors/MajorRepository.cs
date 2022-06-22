using Freelancer_s_Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Majors
{
    public class MajorRepository : Repository<Major>, IMajorRepository
    {
        private readonly FreelancerContext _dbContext;

        public MajorRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
