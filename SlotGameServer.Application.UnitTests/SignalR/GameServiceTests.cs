using Moq;
using Xunit;
using System;
using Microsoft.AspNetCore.SignalR;
using SlotGameServer.Application.Services;
using SlotGameServer.Application.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameServer.Application.UnitTests.SignalR
{
    public class GameServiceTests
    {
        private readonly Mock<IHubContext<GameUpdatesHub>> _mockHubContext;
        private readonly Mock<IClientProxy> _mockClientProxy;
        private readonly Mock<IHubClients> _mockClients;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _mockHubContext = new Mock<IHubContext<GameUpdatesHub>>();
            _mockClientProxy = new Mock<IClientProxy>();
            _mockClients = new Mock<IHubClients>();

            _mockClients.Setup(clients => clients.All).Returns(_mockClientProxy.Object);
            _mockHubContext.Setup(hub => hub.Clients).Returns(_mockClients.Object);

            _gameService = new GameService(_mockHubContext.Object);
        }

        [Fact]
        public async Task UpdateGameStatus_WhenCalled_SendsGameUpdateToAllClients()
        {
            // Arrange
            int gameId = 1;
            string statusMessage = "Game has started";

            // Act
            await _gameService.UpdateGameStatus(gameId, statusMessage);

            // Assert
            _mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "ReceiveGameUpdate",
                    It.Is<object[]>(o => o != null && o.Length == 1 && (string)o[0] == statusMessage),
                    default),
                Times.Once);
        }

        [Fact]
        public async Task NotifyPlayers_WhenCalled_SendsNotificationToAllClients()
        {
            // Arrange
            string notification = "This is a notification";

            // Act
            await _gameService.NotifyPlayers(notification);

            // Assert
            _mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "ReceiveNotification",
                    It.Is<object[]>(o => o != null && o.Length == 1 && (string)o[0] == notification),
                    default),
                Times.Once);
        }
    }

}
