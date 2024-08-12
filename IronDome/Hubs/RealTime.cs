using Microsoft.AspNetCore.SignalR;

namespace IronDome.Hubs
{
	public class RealTime : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
