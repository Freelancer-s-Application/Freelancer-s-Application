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

namespace Freelancer_s_Web.Pages.Profile
{
    [Authorized("USER")]
    public class EditModel : PageModel
    {
        private IHostingEnvironment _environment;

        private UnitOfWorkFactory _unitOfWorkFactory;

        public EditModel(UnitOfWorkFactory unitOfWorkFactory, IHostingEnvironment environment)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _environment = environment;
        }

        [BindProperty]
        public User User { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Choose file to Upload")]
        [BindProperty]
        public IFormFile FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                User = await work.UserRepository.GetCurrentUser();
                ViewData["MajorId"] = new SelectList(work.MajorRepository.GetDbSet(), "Id", "Name");
                return Page();
            } 
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (FileUpload != null)
                {
                    var file = Path.Combine(_environment.WebRootPath, "images", "avatars", FileUpload.FileName);

                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        User.Avatar = FileUpload.FileName;
                        await FileUpload.CopyToAsync(fileStream);
                    }
                }

                using (var work = _unitOfWorkFactory.Get)
                {
                    await work.UserRepository.UpdateUser(User);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!UserExists(User.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return RedirectToPage("./Index");
        }

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
