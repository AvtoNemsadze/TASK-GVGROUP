using MediatR;

namespace SlotGameServer.Application.Spin.Commands.Games
{
    public class SpinCommand : IRequest<SpinResult>
    {
        // public int UserId { get; set; }
        public int ChosenNumber { get; set; }
        public decimal BetAmount { get; set; }
    }
}
