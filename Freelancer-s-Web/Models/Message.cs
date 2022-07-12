using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.Models
{
    public partial class Message : Entity
    {
        public Message()
        {
        }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public virtual User Receiver { get; set; }
        public virtual User Sender { get; set; }
    }
}
