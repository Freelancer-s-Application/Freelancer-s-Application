using Freelancer_s_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Messages
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Dictionary<int, Message>> GetCompanionsAsync();

        Task<List<KeyValuePair<int, Message>>> GetConversationAsync(int id);

        Task SendMessage(int id, string messageContent);
    }
}
