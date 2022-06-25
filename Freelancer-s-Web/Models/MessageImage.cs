using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.Models
{
    public partial class MessageImage : Entity
    {
        public int Messageid { get; set; }
        public string Url { get; set; }

        public virtual Message Message { get; set; }
    }
}
