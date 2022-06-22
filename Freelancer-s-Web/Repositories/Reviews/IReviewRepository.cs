using Freelancer_s_Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Reviews
{
    public interface IReviewRepository : IRepository<Review>
    {
    }
}
