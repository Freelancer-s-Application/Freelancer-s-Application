using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Freelancer_s_Web.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendMessage(string username, string message)
        {
			await Clients.All.SendAsync(username, message);
        }
	}
}

