using Microsoft.AspNetCore.SignalR;
using SlotGameServer.Application.SignalR;

namespace SlotGameServer.Application.Services
{
    public class PromotionService
    {
        private readonly IHubContext<PromotionsHub> _hubContext;

        public PromotionService(IHubContext<PromotionsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task CheckAndSendPromotions(int userId)
        {
            bool sendPromotion = true;

            if (sendPromotion)
            {
                await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePromotion", "Congratulations! You've earned a free spin!");
            }
        }
    }

}
