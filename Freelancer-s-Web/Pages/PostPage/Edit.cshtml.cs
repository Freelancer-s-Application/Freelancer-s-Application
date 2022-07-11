﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Utils;
using Freelancer_s_Web.Commons;

namespace Freelancer_s_Web.Pages.PostPage
{
    [Authorized("USER,ADMIN")]
    public class EditModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;


        public EditModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [BindProperty]
        public Post Post { get; set; }
        public IList<Major> majorList;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var work = _unitOfWorkFactory.Get)
            {
                Post = await work.PostRepository.GetPost(id);
                if (Post.UserId != CustomAuthorization.loginUser.Id && CustomAuthorization.loginUser.Role != CommonEnums.ROLE.ADMINISTRATOR)
                {
                    return Redirect("/Unauthorized");
                }
            }

            if (Post == null)
            {
                return NotFound();
            }
            if (Post.IsDeleted && CustomAuthorization.loginUser.Role != CommonEnums.ROLE.ADMINISTRATOR)
            {
                return NotFound();
            }
            using (var work = _unitOfWorkFactory.Get)
            {
                majorList = work.MajorRepository.GetAll().ToList();
                ViewData["MajorId"] = new SelectList(majorList, "Id", "Name");
                ViewData["UserId"] = CustomAuthorization.loginUser.Id;
            }
                
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                using (var work = _unitOfWorkFactory.Get)
                {
                    var post = await work.PostRepository.GetPost(Post.Id);
                    if (post == null)
                    {
                        return NotFound();
                    }
                    if (post.UserId != CustomAuthorization.loginUser.Id && CustomAuthorization.loginUser.Role != CommonEnums.ROLE.ADMINISTRATOR)
                    {
                        return Redirect("/Unauthorized");
                    }
                    post.Title = Post.Title;
                    post.MajorId = Post.MajorId;
                    post.Description = Post.Description;
                    post.UpdatedAt = DateTime.Now;
                    post.UpdatedBy = CustomAuthorization.loginUser.Email;
                    work.PostRepository.UpdatePost(post);
                    work.Save();
                    return Redirect("/PostPage/Details?id=" + Post.Id);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("/Index");
            }
            return RedirectToPage("./Index");
        }
    }
}
