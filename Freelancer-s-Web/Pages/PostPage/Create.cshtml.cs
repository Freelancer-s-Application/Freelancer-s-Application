using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Utils;
using Freelancer_s_Web.Commons;

namespace Freelancer_s_Web.Pages.PostPage
{
    public class CreateModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public CreateModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public IList<Major> majorList;
        public IActionResult OnGet()
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                majorList = work.MajorRepository.GetAll().ToList();
                ViewData["MajorId"] = new SelectList(majorList, "Id", "Name");
                ViewData["UserId"] = CustomAuthorization.loginUser.Id;
                return Page();
            }
        }

        [BindProperty]
        public Post Post { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Post.UserId = CustomAuthorization.loginUser.Id;
                Post.CreatedAt = DateTime.Now;
                Post.CreatedBy = CustomAuthorization.loginUser.Email;
                Post.Status = CommonEnums.POST_STATUS.PUBLIC;
                using (var work = _unitOfWorkFactory.Get)
                {
                    await work.PostRepository.CreatePost(Post);
                }

                return RedirectToPage("./Index");
            } catch (Exception ex)
            {
                using (var work = _unitOfWorkFactory.Get)
                {
                    majorList = work.MajorRepository.GetAll().ToList();
                    ViewData["MajorId"] = new SelectList(majorList, "Id", "Name");
                    ViewData["UserId"] = CustomAuthorization.loginUser.Id;
                    ViewData["Error"] = ex.Message;
                    return Page();
                }
            }
            
        }
    }
}
