using AutoMapper;
using MediatR;
using SlotGameServer.Application.Spin.Commands.Games;
using System;
using System.Threading;
using System.Threading.Tasks;

public class SpinCommandHandler : IRequestHandler<SpinCommand, SpinResult>
{
    // Assuming you have a DbContext or similar persistence mechanism injected

    //private readonly IMapper _mapper;
    //private readonly IUnitOfWork _unitOfWork;

    //public SpinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //{
    //    _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    //    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    //}

    public async Task<SpinResult> Handle(SpinCommand request, CancellationToken cancellationToken)
    {
        var rng = new Random();
        int resultNumber = rng.Next(1, 11);  // Random number between 1 and 10
        bool isWin = resultNumber == request.ChosenNumber;

        decimal winnings = 0;
        if (isWin)
        {
            winnings = request.BetAmount * 2;  // Double the bet amount for winnings
        }

        // Update user balance, record transaction, etc.
        // (This logic would go here, including database updates)

        return new SpinResult
        {
            IsWin = isWin,
            ResultNumber = resultNumber,
            Winnings = winnings
        };
    }
}
