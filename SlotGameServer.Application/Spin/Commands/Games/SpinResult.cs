using System;


namespace SlotGameServer.Application.Spin.Commands.Games
{
    public class SpinResult
    {
        public bool IsWin { get; set; }  
        public int ResultNumber { get; set; } 
        public decimal BetAmount { get; set; } 
        public decimal Winnings { get; set; } 
        public string? Message { get; set; }
    }

    public class InsufficientBalanceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public InsufficientBalanceResult()
        {
            Success = false;
            Message = "Insufficient balance to place the bet.";
        }
    }
}
