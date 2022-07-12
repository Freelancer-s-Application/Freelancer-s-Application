using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.Commons;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.ViewModel;
using Freelancer_s_Web.Utils;

namespace Freelancer_s_Web.Pages.HomePage
{
    public class IndexModel : PageModel
    {

        private UnitOfWorkFactory _unitOfWorkFactory;
        public IndexModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<Post> Post { get; set; }
        public LoginUserVM LoginUser { get; set; }
        
        public async Task OnGetAsync()
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = await work.PostRepository.GetAll(post => post.Status == CommonEnums.POST_STATUS.PUBLIC && !post.IsDeleted, null, "User,Major,Comments").ToListAsync();
                LoginUser = CustomAuthorization.loginUser;
            }
        }
        public async Task OnGetMyselfAsync()
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = await work.PostRepository.GetAll(post => post.UserId == CustomAuthorization.loginUser.Id, null, "User,Major,Comments").ToListAsync();
                LoginUser = CustomAuthorization.loginUser;
            }
        }

        public async Task OnGetApprovedAsync()
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = new List<Post>();
                var posts = await work.PostRepository.GetAll(post => post.UserId != CustomAuthorization.loginUser.Id && !post.IsDeleted, null, "User,Major,Comments,ApplicationForms").ToListAsync();
                foreach(var post in posts)
                {
                    foreach(var form in post.ApplicationForms)
                    {
                        if(form.UserId == CustomAuthorization.loginUser.Id && form.Status == CommonEnums.APPLICATION_FORM_STATUS.APPROVED)
                        {
                            Post.Add(post);
                            break;
                        }
                    }
                }
                LoginUser = CustomAuthorization.loginUser;
            }
        }
    }
}
