using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class Post : Entity
    {
        public Post()
        {
            ApplicationForms = new HashSet<ApplicationForm>();
            Comments = new HashSet<Comment>();
            PostContents = new HashSet<PostContent>();
        }

        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public int Status { get; set; }
        public int? MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ApplicationForm> ApplicationForms { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostContent> PostContents { get; set; }
    }
}
