using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.Models
{
    public partial class Comment : Entity
    {
        public Comment()
        {
            //InverseParentComment = new HashSet<Comment>();
        }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        //public int? ParentCommentId { get; set; }

        //public virtual Comment ParentComment { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        //public virtual ICollection<Comment> InverseParentComment { get; set; }
    }
}
