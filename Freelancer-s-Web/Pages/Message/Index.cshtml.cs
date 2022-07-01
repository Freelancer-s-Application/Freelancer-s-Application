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
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            //_context = context;
        }

        public IList<Models.Message> Message { get;set; }

        public async Task OnGetAsync()
        {
            //Message = await _context.Messages
            //    .Include(m => m.Receiver)
            //    .Include(m => m.Sender).ToListAsync();
        }
    }
}
