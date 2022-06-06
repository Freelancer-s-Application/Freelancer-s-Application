using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class Review
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }
        public double RatingPoint { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual User Reviewee { get; set; }
        public virtual User Reviewer { get; set; }
    }
}
