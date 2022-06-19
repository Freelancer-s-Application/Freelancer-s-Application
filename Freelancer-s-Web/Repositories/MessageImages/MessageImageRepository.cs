using Freelancer_s_Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.MessageImages
{
    public class MessageImageRepository : Repository<MessageImage>, IMessageImageRepository
    {
        private readonly FreelancerContext _dbContext;

        public MessageImageRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
