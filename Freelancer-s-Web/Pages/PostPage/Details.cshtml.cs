using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;

namespace Freelancer_s_Web.Pages.PostPage
{
    public class DetailsModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public DetailsModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = await work.PostRepository.GetPost(id);
            }

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
