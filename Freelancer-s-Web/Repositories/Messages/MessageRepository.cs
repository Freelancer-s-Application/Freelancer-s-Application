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
            var messages = await _dbContext.Messages.AsNoTracking().OrderByDescending(m => m.CreatedAt).ToListAsync();
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

        //id: companion id
        public async Task<List<KeyValuePair<int, Message>>> GetConversationAsync(int id)
        {
            var currentUserId = CustomAuthorization.loginUser.Id;
            var messages = await _dbContext.Messages.AsNoTracking().ToListAsync();
            List<KeyValuePair<int, Message>> Conversation = new List<KeyValuePair<int, Message>>();
            foreach (var message in messages)
            {
                if ((message.SenderId == currentUserId && message.ReceiverId == id) || (message.ReceiverId == currentUserId && message.SenderId == id))
                {
                    if (message.ReceiverId == currentUserId) message.IsSeen = true;
                    Conversation.Add(new KeyValuePair<int, Message>(message.SenderId, message));
                }
            }
            return Conversation;
        }

        public async Task SendMessage(int id, string messageContent)
        {
            var currentUser = CustomAuthorization.loginUser;
            //var receiver = await _dbContext.Users.FindAsync(id);
            Message message = new Message { Content = messageContent, ReceiverId = id, SenderId = currentUser.Id, IsSeen = false, CreatedAt = DateTime.Now, CreatedBy = currentUser.Email, IsDeleted = false};

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }
    }
}
