using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class ApplicationForm : Entity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Status { get; set; }
        public byte[] Cv { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
