using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class Review : Entity
    {
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }
        public double RatingPoint { get; set; }
        public string Comment { get; set; }

        public virtual User Reviewee { get; set; }
        public virtual User Reviewer { get; set; }
    }
}
