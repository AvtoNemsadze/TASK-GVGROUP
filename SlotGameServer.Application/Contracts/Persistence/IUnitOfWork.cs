namespace SlotGameServer.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IGameBetRepository GameBetRepository { get; }
        IGameSessionRepository GameSessionRepository { get; }
        Task Save();
    }
}
