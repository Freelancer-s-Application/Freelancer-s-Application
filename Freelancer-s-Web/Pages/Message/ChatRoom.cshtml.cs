using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Freelancer_s_Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelancer_s_Web.Pages.Message
{
    [Authorized("USER")]
    public class ChatRoomModel : PageModel
    {
        private UnitOfWorkFactory _unitOfWorkFactory;
        public ChatRoomModel(UnitOfWorkFactory unitOfWorkFactory)
        {
            //_context = context;
            _unitOfWorkFactory = unitOfWorkFactory;
            Conversation = new Dictionary<User, Models.Message>();
        }

        [BindProperty]
        public Dictionary<User, Models.Message> Conversation { get; set; }

        [BindProperty]
        public User currentUser { get; set; }

        [BindProperty]
        public User companion { get; set; }

        public async Task OnGetAsync(int id)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                currentUser = await work.UserRepository.GetCurrentUser();
                companion = work.UserRepository.Get(id);
                var res = await work.MessageRepository.GetConversationAsync(id);
                foreach (var item in res)
                {
                    Conversation.Add(work.UserRepository.Get(item.Key), item.Value);
                }
            }
        }
    }
}
