using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;

namespace Freelancer_s_Web.Pages.ApplicationPages
{
    public class DetailModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public DetailModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public ApplicationForm ApplicationForm { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var work = _unitOfWorkFactory.Get)
            {
                ApplicationForm = work.ApplicationFormRepository.GetFirstOrDefault(app => app.Id == id, "Post,User");
            }

            if (ApplicationForm == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
