using System;


namespace SlotGameServer.Domain.Entities
{
    public class GameBetEntity : BaseEntity
    {
        public int SessionId { get; set; }
        public decimal BetAmount { get; set; }
        public int ChosenNumber { get; set; }
        public int ResultNumber { get; set; }
        public bool IsWin { get; set; }

        // Navigation property to GameSessionEntity
        public virtual GameSessionEntity GameSession { get; set; }
    }
}
