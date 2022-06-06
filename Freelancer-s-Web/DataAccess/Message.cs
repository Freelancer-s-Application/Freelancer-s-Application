using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class Message
    {
        public Message()
        {
            MessageImages = new HashSet<MessageImage>();
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual User Receiver { get; set; }
        public virtual User Sender { get; set; }
        public virtual ICollection<MessageImage> MessageImages { get; set; }
    }
}
