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

namespace Freelancer_s_Web.Pages.PostPage
{
    public class EditModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        private readonly Freelancer_s_Web.Models.FreelancerContext _context;

        public EditModel(Freelancer_s_Web.Models.FreelancerContext context, UnitOfWorkFactory unitOfWorkFactory)
        {
            _context = context;
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
                Post = await work.PostRepository.GetPost(id);
            }

            if (Post == null)
            {
                return NotFound();
            }
           ViewData["MajorId"] = new SelectList(_context.Majors, "Id", "Name");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "DisplayName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using (var work = _unitOfWorkFactory.Get)
                {
                    await work.PostRepository.UpdatePost(Post);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
