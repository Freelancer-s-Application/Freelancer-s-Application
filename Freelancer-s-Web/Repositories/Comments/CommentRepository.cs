﻿using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Comments
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly FreelancerContext _dbContext;

        public CommentRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
