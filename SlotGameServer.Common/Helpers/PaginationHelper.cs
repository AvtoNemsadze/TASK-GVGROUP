using Microsoft.EntityFrameworkCore;
using SlotGameServer.Common.Models;

namespace SlotGameServer.Common.Helpers
{
    public class PaginationHelper<T>
    {
        public async Task<(List<T> Items, PaginationMetadata Pagination)> PaginateAsync(
            IQueryable<T> query,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var totalItemCount = await query.CountAsync(cancellationToken);

            var metadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, metadata);
        }
    }
}
