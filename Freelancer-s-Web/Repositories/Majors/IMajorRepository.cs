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
    public interface IMajorRepository : IRepository<Major>
    {
        DbSet<Major> GetDbSet();
    }
}
