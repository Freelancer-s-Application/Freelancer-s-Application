using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;

namespace Freelancer_s_Web.Pages.Profile
{
    public class IndexModel : PageModel
    {
        //private readonly Freelancer_s_Web.Models.FreelancerContext _context;

        private UnitOfWorkFactory _unitOfWorkFactory;

        public IndexModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            //_context = context;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //User = await _context.Users
            //    .Include(u => u.Major).FirstOrDefaultAsync(m => m.Id == id);

            using (var work = _unitOfWorkFactory.Get)
            {
                User = await work.UserRepository.GetUser(id);
            }

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
