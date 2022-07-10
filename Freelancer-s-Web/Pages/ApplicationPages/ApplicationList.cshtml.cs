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
using Freelancer_s_Web.Commons;

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
                if(Post.UserId != CustomAuthorization.loginUser.Id)
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

        public IActionResult OnPostApprove(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                ApplicationForm form = work.ApplicationFormRepository.GetFirstOrDefault(a => a.Id == id, "User,Post");
                if (form == null)
                {
                    return NotFound();
                }
                Post = work.PostRepository.Get(form.PostId);
                var listApproved = work.ApplicationFormRepository.GetAll(f => f.PostId == form.PostId && f.Status == CommonEnums.APPLICATION_FORM_STATUS.APPROVED).ToList();
                form.Status = CommonEnums.APPLICATION_FORM_STATUS.APPROVED;
                form.UpdatedAt = DateTime.Now;
                form.UpdatedBy = CustomAuthorization.loginUser.Email;
                work.ApplicationFormRepository.UpdateForm(form);
                work.Save();
                ApplicationForm = work.ApplicationFormRepository.GetAllFormByPostIdExceptCV(form.PostId).ToList();
                Post = work.PostRepository.Get(form.PostId);
                return Page();
            }
        }

        public IActionResult OnPostCancel(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                ApplicationForm form = work.ApplicationFormRepository.GetFirstOrDefault(a => a.Id == id, "User,Post");
                if (form == null)
                {
                    return NotFound();
                }
                form.Status = CommonEnums.APPLICATION_FORM_STATUS.CANCELED;
                form.UpdatedAt = DateTime.Now;
                form.UpdatedBy = CustomAuthorization.loginUser.Email;
                work.ApplicationFormRepository.UpdateForm(form);
                work.Save();
                ApplicationForm = work.ApplicationFormRepository.GetAllFormByPostIdExceptCV(form.PostId).ToList();
                Post = work.PostRepository.Get(form.PostId);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmAndClosePostAsync(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Post post = work.PostRepository.GetFirstOrDefault(a => a.Id == id);
                if (post == null)
                {
                    return NotFound();
                }
                post.Status = CommonEnums.POST_STATUS.PRIVATE;
                post.UpdatedAt = DateTime.Now;
                post.UpdatedBy = CustomAuthorization.loginUser.Email;
                await work.PostRepository.UpdatePost(post);
                work.Save();
                ApplicationForm = work.ApplicationFormRepository.GetAllFormByPostIdExceptCV(id).ToList();
                Post = work.PostRepository.Get(id);
                return Page();
            }
        }

    }
}
