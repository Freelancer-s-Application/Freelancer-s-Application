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
    [Authorized("USER,ADMIN")]
    public class IndexModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public IndexModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public User User { get; set; }

        public Boolean? IsLoggingIn;


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
            IsLoggingIn = true;
            return Page();
        }

        public IActionResult OnGetDetail(int id)
        {

            using (var work = _unitOfWorkFactory.Get)
            {
                User = work.UserRepository.Get(id);
            }

            if (User == null)
            {
                return NotFound();
            }
            IsLoggingIn = false;
            return Page();
        }

        public IActionResult OnPostReport(int id, string content)
        {
            try
            {
                var work = _unitOfWorkFactory.Get;
                User = work.UserRepository.Get(id);
                if (User == null)
                {
                    return NotFound();
                }
                IsLoggingIn = false;
                Report report = new Report()
                {
                    ReporteeId = id,
                    ReporterId = CustomAuthorization.loginUser.Id,
                    Comment = content,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CustomAuthorization.loginUser.Email,
                };
                work.ReportRepository.Add(report);
                work.Save();
                ViewData["Message"] = "Reported successfully!";
                return Page();
            } catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("/Index");
            }
            
        }
    }
}
