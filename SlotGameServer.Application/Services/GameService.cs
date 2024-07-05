using Microsoft.AspNetCore.SignalR;
using SlotGameServer.Application.SignalR;

namespace SlotGameServer.Application.Services
{
    public class GameService
    {
        private readonly IHubContext<GameUpdatesHub> _gameUpdatesHub;

        public GameService(IHubContext<GameUpdatesHub> gameUpdatesHub)
        {
            _gameUpdatesHub = gameUpdatesHub;
        }

        public async Task UpdateGameStatus(int gameId, string statusMessage)
        {
            await _gameUpdatesHub.Clients.All.SendAsync("ReceiveGameUpdate", statusMessage);
        }

        public async Task NotifyPlayers(string notification)
        {
            await _gameUpdatesHub.Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }
}
