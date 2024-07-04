using MediatR;

namespace SlotGameServer.Application.Spin.Commands.Games
{
    public class SpinCommand : IRequest<object>
    {
        public int CreateUserId { get; set; }
        public int SessionId { get; set; }  
        public int ChosenNumber { get; set; }
        public decimal BetAmount { get; set; }
    }
}
