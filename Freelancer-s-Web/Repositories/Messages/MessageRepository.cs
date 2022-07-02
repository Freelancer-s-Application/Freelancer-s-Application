using Freelancer_s_Web.Models;
using Freelancer_s_Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Messages
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly FreelancerContext _dbContext;

        public MessageRepository(FreelancerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dictionary<int, Message>> GetCompanionsAsync()
        {
            var currentUserId = CustomAuthorization.loginUser.Id;
            var messages = await _dbContext.Messages.AsNoTracking().ToListAsync();
            Dictionary<int, Message> Companions = new Dictionary<int, Message>();
            foreach (var message in messages)
            {
                if (message.SenderId == currentUserId)
                {
                    if (!Companions.ContainsKey(message.ReceiverId))
                    Companions.Add(message.ReceiverId, message);
                }
                else if (message.ReceiverId == currentUserId)
                {
                    if (!Companions.ContainsKey(message.SenderId))
                        Companions.Add(message.SenderId, message);
                }
            }
            return Companions;
        }

        public async Task<Dictionary<int, Message>> GetConversationAsync(int id)
        {
            var currentUserId = CustomAuthorization.loginUser.Id;
            var messages = await _dbContext.Messages.AsNoTracking().ToListAsync();
            Dictionary<int, Message> Conversation = new Dictionary<int, Message>();
            foreach (var message in messages)
            {
                if ((message.SenderId == currentUserId && message.ReceiverId == id) || (message.ReceiverId == currentUserId && message.SenderId == id))
                {
                    if (Conversation.ContainsKey(message.SenderId)) Conversation[message.SenderId] = message;
                    else Conversation.Add(message.SenderId, message);
                }
            }

            return Conversation;
        }

        public Task SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
