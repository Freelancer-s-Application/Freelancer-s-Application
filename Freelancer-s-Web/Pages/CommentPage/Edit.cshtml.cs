using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Utils;

namespace Freelancer_s_Web.Pages.CommentPage
{
    [Authorized("USER,ADMIN")]
    public class EditModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public EditModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [BindProperty]
        public Comment comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var work = _unitOfWorkFactory.Get)
            {
                comment = work.CommentRepository.GetFirstOrDefault(c => c.Id == id);
                if (comment.Id != CustomAuthorization.loginUser.Id)
                {
                    return Redirect("/Unauthorized");
                }
            }
            if (comment == null)
            {
                return NotFound();
            }
            if (comment.IsDeleted)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                using (var work = _unitOfWorkFactory.Get)
                {
                    var cmt = work.CommentRepository.GetFirstOrDefault(c => c.Id == comment.Id);
                    if (cmt == null) { return NotFound(); }
                    var post = work.PostRepository.GetFirstOrDefault(p => p.Id == comment.Id);
                    cmt.Content = comment.Content;
                    cmt.UpdatedAt = DateTime.Now;
                    cmt.UpdatedBy = CustomAuthorization.loginUser.Email;
                    await work.CommentRepository.UpdateComment(comment);
                    return Redirect("/PostPage/Details?id=" + post.Id);
                }
            }catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return NotFound();
            }
        }
    }
}