using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;

namespace Freelancer_s_Web.Pages.Message
{
    public class DetailsModel : PageModel
    {
        private readonly Freelancer_s_Web.Models.FreelancerContext _context;

        public DetailsModel(Freelancer_s_Web.Models.FreelancerContext context)
        {
            _context = context;
        }

        public Models.Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Messages
                .Include(m => m.Receiver)
                .Include(m => m.Sender).FirstOrDefaultAsync(m => m.Id == id);

            if (Message == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
