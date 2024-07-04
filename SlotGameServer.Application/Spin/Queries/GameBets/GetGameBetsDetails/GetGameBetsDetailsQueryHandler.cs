using MediatR;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Domain.Entities;

namespace SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsDetails
{
    public class GetGameBetsDetailsQueryHandler : IRequestHandler<GetGameBetsDetailsQuery, GameBetEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGameBetsDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GameBetEntity> Handle(GetGameBetsDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GameBetRepository.Get(request.Id);
        }
    }
}
