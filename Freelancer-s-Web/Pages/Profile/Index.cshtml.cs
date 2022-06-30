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

namespace Freelancer_s_Web.Pages.Profile
{
    [Authorized("USER")]
    public class IndexModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public IndexModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            using (var work = _unitOfWorkFactory.Get)
            {
                User = await work.UserRepository.GetCurrentUser();
            }

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
