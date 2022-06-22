using System;
using System.Collections.Generic;

#nullable disable

namespace Freelancer_s_Web.DataAccess
{
    public partial class Report : Entity
    {
        public int ReporterId { get; set; }
        public int ReporteeId { get; set; }
        public string Comment { get; set; }
        public virtual User Reportee { get; set; }
        public virtual User Reporter { get; set; }
    }
}
