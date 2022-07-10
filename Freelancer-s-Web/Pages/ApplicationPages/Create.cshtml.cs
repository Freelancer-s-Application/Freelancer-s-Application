using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Utils;
using System.IO;
using Freelancer_s_Web.Commons;

namespace Freelancer_s_Web.Pages.ApplicationPages
{
    [Authorized("USER")]
    public class CreateModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public CreateModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
        [BindProperty]
        public Post Post { get; set; }
        public IActionResult OnGet(int postId)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = work.PostRepository.GetFirstOrDefault(post => post.Id == postId, "Major,User");
                if (Post == null || Post.Status != CommonEnums.POST_STATUS.PUBLIC)
                {
                    return NotFound();
                }
                if (Post.User.Id == CustomAuthorization.loginUser.Id)
                {
                    return Redirect("/Authentication/Unauthorized");
                }
                ApplicationForm applicationForm = work.ApplicationFormRepository.GetFirstOrDefault(a => a.UserId == CustomAuthorization.loginUser.Id && a.PostId == postId);
                if (applicationForm != null)
                {
                    TempData["Error"] = "You have applied this post!!!";
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }

        [BindProperty]
        public ApplicationForm ApplicationForm { get; set; }

        public async Task<IActionResult> OnPostAsync(int postId)
        {
            ApplicationForm.PostId = postId;
            ApplicationForm.UserId = CustomAuthorization.loginUser.Id;
            ApplicationForm.Status = CommonEnums.APPLICATION_FORM_STATUS.PENDING;
            ApplicationForm.CreatedAt = DateTime.Now;
            ApplicationForm.CreatedBy = CustomAuthorization.loginUser.Email;
            using (var memoryStream = new MemoryStream())
            {
                await FileUpload.FormFile.CopyToAsync(memoryStream);

                // Upload the file if less than 4 MB
                if (memoryStream.Length < 4194304)
                {
                    ApplicationForm.Cv = memoryStream.ToArray();
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                    using (var work = _unitOfWorkFactory.Get)
                    {
                        Post = work.PostRepository.GetFirstOrDefault(post => post.Id == postId, "Major,User");
                    }
                    return Page();
                }
            }
            using (var work = _unitOfWorkFactory.Get)
            {
                work.ApplicationFormRepository.Add(ApplicationForm);
                work.Save();
            }
            return Redirect("/HomePage/Index");
        }
    }
}
