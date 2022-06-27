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
        public Post Post { get; set; }
        public IActionResult OnGet(int postId)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = work.PostRepository.GetFirstOrDefault(post => post.Id == postId, "Major,User");
                if (Post == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        [BindProperty]
        public ApplicationForm ApplicationForm { get; set; }

        public IActionResult OnPost()
        {
            ApplicationForm.PostId = Post.Id;
            ApplicationForm.UserId = CustomAuthorization.loginUser.Id;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using (var work = _unitOfWorkFactory.Get)
            {
                work.ApplicationFormRepository.Add(ApplicationForm);
                work.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}
