using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;

namespace Freelancer_s_Web.Pages.PostPage
{
    public class CreateModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        private readonly Freelancer_s_Web.Models.FreelancerContext _context;

        public CreateModel(Freelancer_s_Web.Models.FreelancerContext context, UnitOfWorkFactory unitOfWorkFactory)
        {
            _context = context;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IActionResult OnGet()
        {
        ViewData["MajorId"] = new SelectList(_context.Majors, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "DisplayName");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var work = _unitOfWorkFactory.Get)
            {
                await work.PostRepository.CreatePost(Post);
            }

            return RedirectToPage("./Index");
        }
    }
}
