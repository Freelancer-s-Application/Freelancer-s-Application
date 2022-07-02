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

namespace Freelancer_s_Web.Pages.Message
{
    [Authorized("USER")]
    public class IndexModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public IndexModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            //_context = context;
            _unitOfWorkFactory = unitOfWorkFactory;
            Companions = new Dictionary<User, Models.Message>();
        }

        public IList<Models.Message> Messages { get;set; }

        [BindProperty]
        public Dictionary<User, Models.Message> Companions { get; set; }

        public async Task OnGetAsync()
        {
            using(var work = _unitOfWorkFactory.Get)
            {
                var res = await work.MessageRepository.GetCompanionsAsync();
                foreach(var item in res)
                {
                    Companions.Add(work.UserRepository.Get(item.Key), item.Value);
                }
            }
        }
    }
}
