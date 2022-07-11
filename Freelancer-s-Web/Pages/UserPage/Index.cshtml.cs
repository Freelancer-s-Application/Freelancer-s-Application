using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.Utils;
using Freelancer_s_Web.UnitOfWork;

namespace Freelancer_s_Web.Pages.UserPage
{
    [Authorized("ADMIN")]
    public class IndexModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public IndexModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<User> User { get;set; }

        public IActionResult OnGetAsync()
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                User = work.UserRepository
                    .GetAll(u => !u.Email.ToLower().Equals(CustomAuthorization.loginUser.Email.ToLower()), null, "Major,ReportReportees,ReportReporters")
                    .ToList();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                try
                {
                    var user = work.UserRepository.GetFirstOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.IsDeleted = true;
                    user.UpdatedAt = DateTime.Now;
                    user.UpdatedBy = CustomAuthorization.loginUser.Email;
                    await work.UserRepository.UpdateUser(user);
                } catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                    return Redirect("/Index");
                }
                User = work.UserRepository
                    .GetAll(u => !u.Email.ToLower().Equals(CustomAuthorization.loginUser.Email.ToLower()), null, "Major")
                    .ToList();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostActivateAsync(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                try
                {
                    var user = work.UserRepository.GetFirstOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.IsDeleted = false;
                    await work.UserRepository.UpdateUser(user);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Something went wrong! Error: " + ex.Message;
                    return Redirect("/Index");
                }
                User = work.UserRepository
                    .GetAll(u => !u.Email.ToLower().Equals(CustomAuthorization.loginUser.Email.ToLower()), null, "Major")
                    .ToList();
                return Page();
            }
        }
    }
}
