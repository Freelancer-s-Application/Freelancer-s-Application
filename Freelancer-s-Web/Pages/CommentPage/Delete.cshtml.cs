using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Commons;
using Freelancer_s_Web.Utils;

namespace Freelancer_s_Web.Pages.CommentPage
{
    [Authorized("USER,ADMIN")]
    public class DeleteModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public DeleteModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        [BindProperty]
        public Comment comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
        }
    }
}
