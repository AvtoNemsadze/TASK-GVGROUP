using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Domain.Entities;
using SlotGameServer.Persistence.Context;


namespace SlotGameServer.Persistence.Repository
{
    public class GameBetRepository : GenericRepository<GameBetEntity>, IGameBetRepository
    {
        private readonly SlotGameServerDbContext _dbContext;
        public GameBetRepository(SlotGameServerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
