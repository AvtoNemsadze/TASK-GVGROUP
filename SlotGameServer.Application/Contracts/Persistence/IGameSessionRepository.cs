using SlotGameServer.Domain.Entities;

namespace SlotGameServer.Application.Contracts.Persistence
{
    public interface IGameSessionRepository : IGenericRepository<GameSessionEntity>
    {
        Task<GameSessionEntity> GetOrCreateSessionAsync(int sessionId, int userId, CancellationToken cancellationToken);
    }
}
