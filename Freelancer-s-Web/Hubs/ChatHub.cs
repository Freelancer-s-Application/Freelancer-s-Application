using Freelancer_s_Web.Models;
using Freelancer_s_Web.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
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
        public async Task SendMessage(string id, string message)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                if (int.TryParse(id, out int receiverId))
                {
                    await work.MessageRepository.SendMessage(receiverId, message);
                }
                await GetMessages(id);
            }
        }

		public async Task GetMessages(string userId)
        {
            using (var work = _unitOfWorkFactory.Get)
            {
                var conversations = await work.MessageRepository.GetConversationAsync(Int32.Parse(userId));
                List<KeyValuePair<User, Message>> res = new List<KeyValuePair<User, Message>>();
                conversations.ForEach(item =>
                {
                    res.Add(new KeyValuePair<User, Message>(work.UserRepository.Get(item.Key), item.Value));
                });

                await Clients.All.SendAsync("GetMessagesResponse", res);
            }
        }

    }
}

