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

namespace Freelancer_s_Web.Pages.PostPage
{
    [Authorized("ADMIN")]
    public class ListFormModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;

        public ListFormModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<ApplicationForm> ApplicationForm { get;set; }

        // param: post id to view list applied forms
        public async Task OnGetAsync(int id)
        {
            using(var work = _unitOfWorkFactory.Get)
            {
                ApplicationForm = await work.ApplicationFormRepository.GetAll(f => f.PostId == id, null, "Post,User").ToListAsync();
            }
        }
    }
}
