using MediatR;
using SlotGameServer.Application.Spin.Commands.Games;


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
        string message;

        if (isWin)
        {
            winnings = request.BetAmount * 2;  // Double the bet amount for winnings
            message = $"Congratulations! You won {winnings}!";
        }
        else
        {
            winnings = 0;
            message = "Sorry, you lost. Better luck next time!";
        }

        // Here you would typically update the user's balance in your database
        // For example, increasing the balance if they win, and decrementing if they lose

        return new SpinResult
        {
            IsWin = isWin,
            ResultNumber = resultNumber,
            BetAmount = request.BetAmount,
            Winnings = winnings,
            Message = message
        };
    }


    //public class SpinCommandHandler2 : IRequestHandler<SpinCommand, SpinResult>
    //{
    //    private readonly SlotGameServerDbContext _context;

    //    public SpinCommandHandler2(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<SpinResult> Handle(SpinCommand request, CancellationToken cancellationToken)
    //    {
    //        var rng = new Random();
    //        int resultNumber = rng.Next(1, 11);  // Random number between 1 and 10
    //        bool isWin = resultNumber == request.ChosenNumber;
    //        decimal winnings = isWin ? request.BetAmount * 2 : 0;
    //        string message = isWin ? $"Congratulations! You won {winnings}!" : "Sorry, you lost. Better luck next time!";

    //        // Find or create a new game session
    //        GameSession session = await _context.GameSessions
    //                                    .FirstOrDefaultAsync(s => s.SessionId == request.SessionId && s.UserId == request.UserId, cancellationToken)
    //                                    ?? new GameSession { UserId = request.UserId, CreatedAt = DateTime.UtcNow };

    //        // If session is new, add it to the context
    //        if (session.SessionId == 0)
    //            _context.GameSessions.Add(session);

    //        // Wait until changes are saved to assign an ID to a new session
    //        await _context.SaveChangesAsync(cancellationToken);

    //        // Create a new game bet
    //        var bet = new GameBet
    //        {
    //            SessionId = session.SessionId,  // Ensure the SessionId is assigned
    //            BetAmount = request.BetAmount,
    //            ChosenNumber = request.ChosenNumber,
    //            ResultNumber = resultNumber,
    //            IsWin = isWin,
    //            CreatedAt = DateTime.UtcNow
    //        };

    //        _context.GameBets.Add(bet);

    //        // Update user balance
    //        var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
    //        if (user != null)
    //        {
    //            if (isWin)
    //                user.Balance += winnings;  // Increase balance by winnings
    //            else
    //                user.Balance -= request.BetAmount;  // Deduct the bet amount from balance

    //            await _context.SaveChangesAsync(cancellationToken);
    //        }

    //        return new SpinResult
    //        {
    //            IsWin = isWin,
    //            ResultNumber = resultNumber,
    //            BetAmount = request.BetAmount,
    //            Winnings = winnings,
    //            Message = message
    //        };
    //    }
    //}
}
