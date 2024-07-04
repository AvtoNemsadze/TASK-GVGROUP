using MediatR;


namespace SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsList
{
    public class GetAllGameBetsQuery : IRequest<PaginatedResponse<GetGameBetsListModel>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetAllGameBetsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
