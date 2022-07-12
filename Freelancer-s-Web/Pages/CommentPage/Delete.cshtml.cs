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

namespace Freelancer_s_Web.Pages.CommentPage
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
        public Comment comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            using(var work = _unitOfWorkFactory.Get)
            {
                comment = work.CommentRepository.GetFirstOrDefault(c => c.Id == id);    
            }
            if(comment == null)
            {
                return NotFound();
            }
            if (comment.IsDeleted)
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
                comment = work.CommentRepository.GetFirstOrDefault(p => p.Id == id);
                var post = work.PostRepository.GetFirstOrDefault(p => p.Id == comment.PostId);
                if (comment == null)
                {
                    return NotFound();
                }
                comment.IsDeleted = true;
                comment.UpdatedAt = DateTime.Now;
                comment.UpdatedBy = CustomAuthorization.loginUser.Email;
                await work.CommentRepository.UpdateComment(comment);
                return Redirect("/PostPage/Details?id=" + post.Id); 
            }
            
        }
    }
}
