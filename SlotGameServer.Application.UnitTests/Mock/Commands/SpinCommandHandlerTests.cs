using Moq;
using Shouldly;
using SlotGameServer.Application.Contracts.Identity;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Application.Spin.Commands.Games;
using SlotGameServer.Domain.Entities;
using Xunit;

namespace SlotGameServer.Application.UnitTests.Mock.Commands
{
    public class SpinCommandHandlerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGameSessionRepository> _mockGameSessionRepository;
        private readonly Mock<IGameBetRepository> _mockGameBetRepository;
        private SpinCommandHandler _handler;

        public SpinCommandHandlerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockGameSessionRepository = new Mock<IGameSessionRepository>();
            _mockGameBetRepository = new Mock<IGameBetRepository>();

            _mockUnitOfWork.Setup(u => u.GameSessionRepository).Returns(_mockGameSessionRepository.Object);
            _mockUnitOfWork.Setup(u => u.GameBetRepository).Returns(_mockGameBetRepository.Object);

            _handler = new SpinCommandHandler(_mockUnitOfWork.Object, _mockUserService.Object);
        }

        [Fact]
        public async Task Handle_UserHasInsufficientBalance_ReturnsInsufficientBalanceResult()
        {
            // Arrange
            var handler = new SpinCommandHandler(_mockUnitOfWork.Object, _mockUserService.Object);
            var spinCommand = new SpinCommand { CreateUserId = 1, BetAmount = 100 };
            _mockUserService.Setup(svc => svc.CanUserPlaceBetAsync(It.IsAny<int>(), It.IsAny<decimal>()))
                            .ReturnsAsync(false); // User does not have sufficient balance

            // Act
            var result = await handler.Handle(spinCommand, new CancellationToken());

            // Assert
            result.ShouldBeOfType<InsufficientBalanceResult>();
        }

        [Fact]
        public async Task Handle_ValidRequest_GetsOrCreatesSession()
        {
            // Arrange
            var spinCommand = new SpinCommand { CreateUserId = 1, BetAmount = 100, SessionId = 1 };
            var expectedSession = new GameSessionEntity { Id = 1, CreateUserId = 1 };

            _mockUserService.Setup(s => s.CanUserPlaceBetAsync(It.IsAny<int>(), It.IsAny<decimal>())).ReturnsAsync(true);
            _mockGameSessionRepository.Setup(r => r.GetOrCreateSessionAsync(spinCommand.SessionId, spinCommand.CreateUserId, It.IsAny<CancellationToken>()))
                                      .ReturnsAsync(expectedSession);
            // Act
            await _handler.Handle(spinCommand, new CancellationToken());

            // Assert
            _mockGameSessionRepository.Verify(r => r.GetOrCreateSessionAsync(spinCommand.SessionId, spinCommand.CreateUserId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidBet_PlacesBetWithCorrectParameters()
        {
            // Arrange
            var spinCommand = new SpinCommand { CreateUserId = 1, BetAmount = 100, ChosenNumber = 5, SessionId = 1 };
            var fakeSession = new GameSessionEntity { Id = 1, CreateUserId = 1 };
            _mockUserService.Setup(s => s.CanUserPlaceBetAsync(It.IsAny<int>(), It.IsAny<decimal>())).ReturnsAsync(true);
            _mockGameSessionRepository.Setup(r => r.GetOrCreateSessionAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                      .ReturnsAsync(fakeSession);

            // Act
            await _handler.Handle(spinCommand, new CancellationToken());

            // Assert
            _mockGameBetRepository.Verify(r => r.Add(It.Is<GameBetEntity>(b =>
                b.SessionId == fakeSession.Id &&
                b.BetAmount == spinCommand.BetAmount &&
                b.ChosenNumber == spinCommand.ChosenNumber)), Times.Once);
        }
    }
}
