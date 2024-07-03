using System;


namespace SlotGameServer.Domain.Entities
{
    public class GameSessionEntity : BaseEntity
    {
        public List<GameBetEntity> GameBets { get; set; } = new List<GameBetEntity>();
    }
}
