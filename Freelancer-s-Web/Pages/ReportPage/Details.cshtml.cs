using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Utils;

namespace Freelancer_s_Web.Pages.ReportPage
{
    [Authorized("ADMIN")]
    public class DetailsModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public DetailsModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Report Report { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var work = _unitOfWorkFactory.Get)
            {
                Report = work.ReportRepository.GetFirstOrDefault(r => r.Id == id, "Reportee,Reporter");
            }

            if (Report == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
