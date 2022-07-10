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

        public Post Post { get; set; }
        public IEnumerable<PostContent> postContents { get; set; }
        public IEnumerable<Comment> comments { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = await work.PostRepository.GetPost(id);
                
                postContents = await work.PostContentRepository.GetAllPostContentByPostId(id);

                comments = await work.CommentRepository.GetAllCommentByPostId(id);
            }

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
