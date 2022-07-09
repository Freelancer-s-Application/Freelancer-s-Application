using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;

namespace Freelancer_s_Web.Pages.PostPage
{
    //Post page management of admin

    public class IndexModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public IndexModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            //_context = context;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public List<Post> Post { get; set; }

        public async Task OnGetAsync()
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                Post = await work.PostRepository.GetAllPosts();
            }
        }
    }
}
