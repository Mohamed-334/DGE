using DGE.Service.Shared.PaginatedList;
using Microsoft.EntityFrameworkCore;

namespace DGE.Service.Shared.ExtensionMethods
{
    public static class PaginatedListExtension
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(source);

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var count = pageNumber == 1 && items.Count < pageSize
                ? items.Count
                : await source.CountAsync(cancellationToken);

            return PaginatedList<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
