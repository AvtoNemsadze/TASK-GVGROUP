using SlotGameServer.Common.Models;


namespace SlotGameServer.Application.Spin.Queries.GameBets.GetGameBetsList
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public PaginationMetadata Pagination { get; set; }

        public PaginatedResponse(List<T> items, PaginationMetadata pagination)
        {
            Items = items;
            Pagination = pagination;
        }
    }

}
