using Freelancer_s_Web.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Freelancer_s_Web.Hubs
{
	public class ChatHub : Hub
	{
        private UnitOfWorkFactory _unitOfWorkFactory;
        public ChatHub(UnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task SendMessage(string username, string message)
        {
			await Clients.All.SendAsync("ReceiveMessage", username, message, DateTime.Now.ToShortDateString());
        }

		public async Task GetMessages(string userId)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                var conversations = await work.MessageRepository.GetConversationAsync(Int32.Parse(userId));
                await Clients.All.SendAsync("GetMessagesResponse", conversations);
            }
        }
	}
}

