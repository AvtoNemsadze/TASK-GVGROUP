using AutoMapper;
using MediatR;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Domain.Entities;

namespace SlotGameServer.Application.Spin.Commands.Games 
{ 
    public class SpinCommandHandler : IRequestHandler<SpinCommand, SpinResult>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SpinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SpinResult> Handle(SpinCommand request, CancellationToken cancellationToken)
        {
            var rng = new Random();
            int resultNumber = rng.Next(1, 11);  // Random number between 1 and 10
            bool isWin = resultNumber == request.ChosenNumber;
            decimal winnings = isWin ? request.BetAmount * 2 : 0;
            string message = isWin ? $"Congratulations! You won {winnings}!" : "Sorry, you lost. Better luck next time!";

            GameSessionEntity session = await _unitOfWork.GameSessionRepository
                .GetOrCreateSessionAsync(request.SessionId, request.CreateUserId, cancellationToken);

            var bet = new GameBetEntity
            {
                SessionId = session.Id,  
                BetAmount = request.BetAmount,
                ChosenNumber = request.ChosenNumber,
                ResultNumber = resultNumber,
                IsWin = isWin,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GameBetRepository.Add(bet);
            await _unitOfWork.Save();

            // Update user balance
            //var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            //if (user != null)
            //{
            //    if (isWin)
            //        user.Balance += winnings;  // Increase balance by winnings
            //    else
            //        user.Balance -= request.BetAmount;  // Deduct the bet amount from balance

            //    await _context.SaveChangesAsync(cancellationToken);
            //}

            return new SpinResult
            {
                IsWin = isWin,
                ResultNumber = resultNumber,
                BetAmount = request.BetAmount,
                Winnings = winnings,
                Message = message
            };
        }
    }
}
