﻿using Freelancer_s_Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Notifications
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly FreelancerContext _dbContext;

        public NotificationRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
