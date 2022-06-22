using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class Major : Entity
    {
        public Major()
        {
            Posts = new HashSet<Post>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
