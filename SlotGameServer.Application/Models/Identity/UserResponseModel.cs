using System;


namespace SlotGameServer.Application.Models.Identity
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public decimal Balance { get; set; }    
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalGamesPlayed { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
