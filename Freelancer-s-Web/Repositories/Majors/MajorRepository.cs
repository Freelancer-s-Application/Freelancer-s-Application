using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public List<Major> GetAll()
        {
            var dbset = _dbContext.Majors.AsNoTracking().ToList();
            return dbset;
        }
    }
}
