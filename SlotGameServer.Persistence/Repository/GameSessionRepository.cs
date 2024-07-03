using Microsoft.EntityFrameworkCore;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Domain.Entities;
using SlotGameServer.Persistence.Context;


namespace SlotGameServer.Persistence.Repository
{
    public class GameSessionRepository : GenericRepository<GameSessionEntity>, IGameSessionRepository
    {
        private readonly SlotGameServerDbContext _dbContext;
        public GameSessionRepository(SlotGameServerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<GameSessionEntity> GetOrCreateSessionAsync(int sessionId, int userId, CancellationToken cancellationToken)
        {
            var session = await _dbContext.GameSessions
                                          .FirstOrDefaultAsync(s => s.Id == sessionId && s.CreateUserId == userId, cancellationToken);

            if (session == null)
            {
                session = new GameSessionEntity { CreateUserId = userId, CreatedAt = DateTime.UtcNow };
                await Add(session);
            }

            return session;
        }
    }
}
