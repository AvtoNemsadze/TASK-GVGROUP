using Microsoft.AspNetCore.SignalR;

namespace SlotGameServer.Application.SignalR
{
    public class PromotionsHub : Hub
    {
        public async Task SendPromotion(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceivePromotion", message);
        }
    }
}
