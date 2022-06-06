﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class User
    {
        public User()
        {
            ApplicationForms = new HashSet<ApplicationForm>();
            Comments = new HashSet<Comment>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            NotificationReceivers = new HashSet<Notification>();
            NotificationSenders = new HashSet<Notification>();
            Posts = new HashSet<Post>();
            ReportReportees = new HashSet<Report>();
            ReportReporters = new HashSet<Report>();
            ReviewReviewees = new HashSet<Review>();
            ReviewReviewers = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public int? MajorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Major Major { get; set; }
        public virtual ICollection<ApplicationForm> ApplicationForms { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Notification> NotificationReceivers { get; set; }
        public virtual ICollection<Notification> NotificationSenders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> ReportReportees { get; set; }
        public virtual ICollection<Report> ReportReporters { get; set; }
        public virtual ICollection<Review> ReviewReviewees { get; set; }
        public virtual ICollection<Review> ReviewReviewers { get; set; }
    }
}
