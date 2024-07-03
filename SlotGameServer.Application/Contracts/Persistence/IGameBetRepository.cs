using SlotGameServer.Domain;
using SlotGameServer.Domain.Entities;

namespace SlotGameServer.Application.Contracts.Persistence
{
    public interface IGameBetRepository : IGenericRepository<GameBetEntity>
    {
    }
}
