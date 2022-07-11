using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.Models
{
    public partial class PostContent : Entity
    {
        public string Type { get; set; }
        public int PostId { get; set; }
        public byte[] File { get; set; }
        public virtual Post Post { get; set; }
    }
}
