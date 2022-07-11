using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Commons;
using Freelancer_s_Web.Utils;

namespace Freelancer_s_Web.Pages.PostPage
{
    [Authorized("USER,ADMIN")]
    public class DeleteModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public DeleteModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = work.PostRepository.GetFirstOrDefault(p => p.Id == id);
            }
            if (Post == null)
            {
                return NotFound();
            }
            if (Post.IsDeleted && CustomAuthorization.loginUser.Role != CommonEnums.ROLE.ADMINISTRATOR)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var work = _unitOfWorkFactory.Get)
            {
                Post = work.PostRepository.GetFirstOrDefault(p => p.Id == id);
                if (Post == null)
                {
                    return NotFound();
                }
                Post.Status = CommonEnums.POST_STATUS.REMOVE;
                Post.IsDeleted = true;
                Post.UpdatedAt = DateTime.Now;
                Post.UpdatedBy = CustomAuthorization.loginUser.Email;
                work.PostRepository.UpdatePost(Post);
                work.Save();
            }
            if (CustomAuthorization.loginUser.Role == CommonEnums.ROLE.ADMINISTRATOR)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/HomePage/Index");
            }
        }
    }
}
