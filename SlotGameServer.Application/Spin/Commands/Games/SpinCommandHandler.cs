using AutoMapper;
using MediatR;
using SlotGameServer.Application.Contracts.Identity;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Domain.Entities;

namespace SlotGameServer.Application.Spin.Commands.Games 
{ 
    public class SpinCommandHandler : IRequestHandler<SpinCommand, object>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public SpinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException();
        }

        public async Task<object> Handle(SpinCommand request, CancellationToken cancellationToken)
        {
            bool hasSufficientBalance = await _userService.CanUserPlaceBetAsync(request.CreateUserId, request.BetAmount);
            if (!hasSufficientBalance)
            {
                return new InsufficientBalanceResult();
            }

            SpinResult spinResult = EvaluateSpinResult(request);
            
            await _userService.UpdatePlayerStatistics(request.CreateUserId, spinResult.IsWin);

            GameSessionEntity session = await _unitOfWork.GameSessionRepository
                .GetOrCreateSessionAsync(request.SessionId, request.CreateUserId, cancellationToken);

            var bet = new GameBetEntity
            {
                SessionId = session.Id,  
                BetAmount = request.BetAmount,
                ChosenNumber = request.ChosenNumber,
                ResultNumber = spinResult.ResultNumber,
                IsWin = spinResult.IsWin,
                CreatedAt = DateTime.UtcNow
            };

            await Task.WhenAll(
                _unitOfWork.GameBetRepository.Add(bet),
                _userService.UpdateBalanceAsync(request.CreateUserId, request.BetAmount, spinResult.IsWin)
            );
            await _unitOfWork.Save();

            return spinResult;
        }

        private static SpinResult EvaluateSpinResult(SpinCommand request)
        {
            var rng = new Random();
            int resultNumber = rng.Next(1, 11);
            bool isWin = resultNumber == request.ChosenNumber;
            decimal winnings = isWin ? request.BetAmount * 2 : 0;
            string message = isWin ? $"Congratulations! You won {winnings}!" : "Sorry, you lost. Better luck next time!";

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
