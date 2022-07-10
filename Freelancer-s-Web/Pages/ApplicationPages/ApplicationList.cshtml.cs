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
        public Post Post { get;set; }

        public IActionResult OnGet(int postId)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                ApplicationForm = work.ApplicationFormRepository.GetAllFormByPostIdExceptCV(postId).ToList();
                Post = work.PostRepository.Get(postId);
                if(Post.User.Id != CustomAuthorization.loginUser.Id)
                {
                    return Redirect("/Authentication/Unauthorized");
                }
                return Page();
            }
        }

        public IActionResult OnGetDownloadCV(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                ApplicationForm form = work.ApplicationFormRepository.GetFirstOrDefault(a => a.Id == id, "User");
                if (form == null)
                {
                    return NotFound();
                }
                ApplicationForm = work.ApplicationFormRepository.GetAllFormByPostIdExceptCV(form.PostId).ToList();
                Post = work.PostRepository.Get(form.PostId);
                return File(form.Cv, "application/pdf", "cv-" + form.User.Email+ ".pdf");
            }
        }
    }
}
