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

namespace Freelancer_s_Web.Pages.ApplicationPages
{
    [Authorized("ADMIN,USER")]
    public class ApplicationListModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public ApplicationListModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<ApplicationForm> ApplicationForm { get;set; }

        public IActionResult OnGet(int postId)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                ApplicationForm = work.ApplicationFormRepository.GetAll(a => a.Post.Id == postId, null, "Post").ToList();
                return Page();
            }
        }
    }
}
