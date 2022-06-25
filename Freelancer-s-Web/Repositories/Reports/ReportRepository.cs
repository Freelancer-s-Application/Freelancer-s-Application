using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Reports
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly FreelancerContext _dbContext;

        public ReportRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
