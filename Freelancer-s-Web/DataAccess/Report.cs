using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class Report
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int ReporteeId { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual User Reportee { get; set; }
        public virtual User Reporter { get; set; }
    }
}
