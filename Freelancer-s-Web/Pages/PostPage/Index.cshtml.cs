using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;

namespace Freelancer_s_Web.Pages.PostPage
{
    public class IndexModel : PageModel
    {
        private readonly Freelancer_s_Web.Models.FreelancerContext _context;

        public IndexModel(Freelancer_s_Web.Models.FreelancerContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = await _context.Posts
                .Include(p => p.Major)
                .Include(p => p.User).ToListAsync();
        }
    }
}
