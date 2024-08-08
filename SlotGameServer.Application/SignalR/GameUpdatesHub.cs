using Microsoft.AspNetCore.SignalR;


namespace SlotGameServer.Application.SignalR
{
    public class GameUpdatesHub : Hub
    {
        public async Task SendGameUpdate(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveGameUpdate", message);
        }

        public async Task NotifyAll(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task SendSpinResult(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveSpinResult", message);
        }
    }
}
