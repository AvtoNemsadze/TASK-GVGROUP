using Moq;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsDetails;
using SlotGameServer.Domain.Entities;
using Xunit;

namespace SlotGameServer.Application.UnitTests.Mock.Queries
{
    public class GetGameBetsDetailsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetGameBetsDetailsQueryHandler _handler;

        public GetGameBetsDetailsQueryHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetGameBetsDetailsQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsGameBetDetails()
        {
            // Arrange
            var gameBet = new GameBetEntity
            {
                Id = 1,
                BetAmount = 100,
                ChosenNumber = 5,
                ResultNumber = 3,
                IsWin = false
            };

            _mockUnitOfWork.Setup(uow => uow.GameBetRepository.Get(It.IsAny<int>()))
                           .ReturnsAsync(gameBet);

            var query = new GetGameBetsDetailsQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gameBet.Id, result.Id);
            Assert.Equal(gameBet.BetAmount, result.BetAmount);
            Assert.Equal(gameBet.ChosenNumber, result.ChosenNumber);
            Assert.Equal(gameBet.ResultNumber, result.ResultNumber);
            Assert.Equal(gameBet.IsWin, result.IsWin);

            _mockUnitOfWork.Verify(uow => uow.GameBetRepository.Get(It.Is<int>(id => id == query.Id)), Times.Once);
        }
    }
}
