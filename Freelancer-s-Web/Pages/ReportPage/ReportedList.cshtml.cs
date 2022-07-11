using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.Utils;
using Freelancer_s_Web.UnitOfWork;

namespace Freelancer_s_Web.Pages.ReportPage
{
    [Authorized("ADMIN")]
    public class ReportedListModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public ReportedListModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<Report> Report { get; set; }

        public async Task OnGetAsync(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Report = work.ReportRepository.GetAll(u => u.ReporteeId == id, null, "Reportee,Reporter").ToList();
            }
        }
    }
}
