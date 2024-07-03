using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Persistence.Context;


namespace SlotGameServer.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SlotGameServerDbContext _context;
        private IGameBetRepository _gameBetRepository;
        private IGameSessionRepository _gameSessionRepository;

        public UnitOfWork(
            SlotGameServerDbContext context,
            IGameBetRepository gameBetRepository,
            IGameSessionRepository gameSessionRepository)
        {
            _context = context;
            _gameBetRepository = gameBetRepository;
            _gameSessionRepository = gameSessionRepository;

        }

        public IGameBetRepository GameBetRepository =>
          _gameBetRepository ??= new GameBetRepository(_context);

        public IGameSessionRepository GameSessionRepository =>
         _gameSessionRepository ??= new GameSessionRepository(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
