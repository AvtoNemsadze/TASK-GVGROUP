using AutoMapper;
using MediatR;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Application.Spin.Commands.Games;
using SlotGameServer.Domain.Entities;


namespace SlotGameServer.Application.Spin.Commands.Sessions
{
    public class StartGameCommandHandler : IRequestHandler<StartGameCommand, StartGameResult>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StartGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StartGameResult> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            var newSession = new GameSessionEntity(); 

            await _unitOfWork.GameSessionRepository.Add(newSession);
            await _unitOfWork.Save();

            return new StartGameResult { SessionId = newSession.Id };
        }

    }
}
