using AutoMapper;
using MediatR;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Common.Helpers;
using SlotGameServer.Domain.Entities;


namespace SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsList
{
    public class GetAllGameBetsQueryHandler : IRequestHandler<GetAllGameBetsQuery, PaginatedResponse<GetGameBetsListModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationHelper<GameBetEntity> _paginationHelper;
        private readonly IMapper _mapper;

        public GetAllGameBetsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paginationHelper = new PaginationHelper<GameBetEntity>();
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<GetGameBetsListModel>> Handle(GetAllGameBetsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.GameBetRepository.GetAllQueryable();
           
            var (items, metadata) = await _paginationHelper.PaginateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
            
            var mappedItems = _mapper.Map<List<GetGameBetsListModel>>(items);

            return new PaginatedResponse<GetGameBetsListModel>(mappedItems, metadata);
        }
    }
}
