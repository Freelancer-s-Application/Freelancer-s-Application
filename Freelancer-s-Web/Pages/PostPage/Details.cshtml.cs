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

namespace Freelancer_s_Web.Pages.PostPage
{
    [Authorized("USER,ADMIN")]
    public class DetailsModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public DetailsModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [BindProperty]
        public Post Post { get; set; }
        public IEnumerable<PostContent> postContents { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public User User { get; set; }

        [BindProperty]
        public Comment comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            comment = new Comment();
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = await work.PostRepository.GetPost(id);
                
                postContents = await work.PostContentRepository.GetAllPostContentByPostId(id);

                comments = await work.CommentRepository.GetAllCommentByPostId(id);

                User = await work.UserRepository.GetCurrentUser();  
            }

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //try
            //{
            //comment = new Comment();
                comment.PostId = Post.Id;
                comment.UserId = CustomAuthorization.loginUser.Id;
                comment.CreatedAt = DateTime.Now;
                comment.CreatedBy = CustomAuthorization.loginUser.Email;
                using (var work = _unitOfWorkFactory.Get)
                {
                    await work.CommentRepository.CreateComment(comment);
                }
                return Redirect("/PostPage/Details?id=" + Post.Id);
                //return Page();
            //}
            //catch (Exception ex)
            //{
            //    ViewData["Error"] = ex.Message;
            //    return Page();
            //}
        }

    }
}
