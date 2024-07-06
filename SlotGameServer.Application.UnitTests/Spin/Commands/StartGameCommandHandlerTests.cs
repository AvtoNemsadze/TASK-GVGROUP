using AutoMapper;
using Moq;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Application.Spin.Commands.Sessions;
using SlotGameServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SlotGameServer.Application.UnitTests.Spin.Commands
{
    public class StartGameCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StartGameCommandHandler _handler;

        public StartGameCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new StartGameCommandHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_AddsNewSessionAndReturnsSessionId()
        {
            // Arrange
            var newSession = new GameSessionEntity { Id = 1 };
            _mockUnitOfWork.Setup(uow => uow.GameSessionRepository.Add(It.IsAny<GameSessionEntity>()))
                           .Callback<GameSessionEntity>(session => session.Id = newSession.Id);
            _mockUnitOfWork.Setup(uow => uow.Save()).Returns(Task.CompletedTask);

            var command = new StartGameCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newSession.Id, result.SessionId);

            _mockUnitOfWork.Verify(uow => uow.GameSessionRepository.Add(It.IsAny<GameSessionEntity>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }
    }
}
