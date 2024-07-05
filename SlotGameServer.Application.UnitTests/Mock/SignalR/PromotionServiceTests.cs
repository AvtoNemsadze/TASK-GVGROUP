using Microsoft.AspNetCore.SignalR;
using Moq;
using SlotGameServer.Application.Services;
using SlotGameServer.Application.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlotGameServer.Application.UnitTests.Mock.SignalR
{
    public class PromotionServiceTests
    {
        private readonly Mock<IHubContext<PromotionsHub>> _mockHubContext;
        private readonly Mock<IClientProxy> _mockClientProxy;
        private readonly Mock<IHubClients> _mockClients;
        private readonly PromotionService _promotionService;

        public PromotionServiceTests()
        {
            _mockHubContext = new Mock<IHubContext<PromotionsHub>>();
            _mockClientProxy = new Mock<IClientProxy>();
            _mockClients = new Mock<IHubClients>();

            _mockClients.Setup(clients => clients.User(It.IsAny<string>())).Returns(_mockClientProxy.Object);
            _mockHubContext.Setup(hub => hub.Clients).Returns(_mockClients.Object);

            _promotionService = new PromotionService(_mockHubContext.Object);
        }

        [Fact]
        public async Task CheckAndSendPromotions_WhenCalled_SendsPromotionToUser()
        {
            // Arrange
            int userId = 1;

            // Act
            await _promotionService.CheckAndSendPromotions(userId);

            // Assert
            _mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "ReceivePromotion",
                    It.Is<object[]>(o => o != null && o.Length == 1 && (string)o[0] == "Congratulations! You've earned a free spin!"),
                    default),
                Times.Once);
        }
    }
}
