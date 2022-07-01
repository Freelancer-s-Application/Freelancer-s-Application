using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Freelancer_s_Web.Models;

namespace Freelancer_s_Web.Pages.PostPage
{
    public class CreateModel : PageModel
    {
        private readonly Freelancer_s_Web.Models.FreelancerContext _context;

        public CreateModel(Freelancer_s_Web.Models.FreelancerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MajorId"] = new SelectList(_context.Majors, "Id", "CreatedBy");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Avatar");
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

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
