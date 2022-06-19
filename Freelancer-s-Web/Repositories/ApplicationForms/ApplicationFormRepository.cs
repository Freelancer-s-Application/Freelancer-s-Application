using Freelancer_s_Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
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
    }
}
