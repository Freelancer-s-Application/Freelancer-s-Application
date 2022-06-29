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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Freelancer_s_Web.Utils;
using Microsoft.Extensions.Logging;

namespace Freelancer_s_Web.Pages.Profile
{
    [Authorized("USER")]
    public class EditModel : PageModel
    {
        private IHostingEnvironment _environment;

        private UnitOfWorkFactory _unitOfWorkFactory;

        [BindProperty]
        public bool _firstTime { get; set; }

        public EditModel(UnitOfWorkFactory unitOfWorkFactory, IHostingEnvironment environment)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _environment = environment;
            _firstTime = false;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(string firstTime)
        {
            if (!String.IsNullOrEmpty(firstTime)) _firstTime = true;
            using (var work = _unitOfWorkFactory.Get)
            {
                User = await work.UserRepository.GetCurrentUser();
                ViewData["MajorId"] = new SelectList(work.MajorRepository.GetAll(), "Id", "Name");
                return Page();
            } 
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                using (var work = _unitOfWorkFactory.Get)
                {
                    await work.UserRepository.UpdateUser(User);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            if (_firstTime) return RedirectToPage("../Index");
            return RedirectToPage("./Index");
        }

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
