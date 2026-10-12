using DGE.Service.Shared.ExtensionMethods;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace DGE.Tests.Pagination
{
    public class PaginatedListExtensionTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly ItemsDbContext _context;
        private readonly CommandRecorder _recorder = new();

        public PaginatedListExtensionTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _context = new ItemsDbContext(new DbContextOptionsBuilder<ItemsDbContext>()
                .UseSqlite(_connection)
                .AddInterceptors(_recorder)
                .Options);
            _context.Database.EnsureCreated();
        }

        private async Task SeedAsync(int count)
        {
            _context.Items.AddRange(Enumerable.Range(1, count).Select(id => new Item { Id = id }));
            await _context.SaveChangesAsync();
            _recorder.Commands.Clear();
        }

        [Fact]
        public async Task ToPaginatedListAsync_ReturnsRequestedPageAndTotals()
        {
            await SeedAsync(25);

            var page = await _context.Items.OrderBy(i => i.Id).ToPaginatedListAsync(2, 10);

            Assert.Equal(Enumerable.Range(11, 10), page.Data.Select(i => i.Id));
            Assert.Equal(25, page.TotalCount);
            Assert.Equal(3, page.TotalPages);
            Assert.True(page.HasPreviousPage);
            Assert.True(page.HasNextPage);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-3, -5)]
        public async Task ToPaginatedListAsync_NonPositiveArguments_FallBackToFirstPageOfTen(int pageNumber, int pageSize)
        {
            await SeedAsync(15);

            var page = await _context.Items.OrderBy(i => i.Id).ToPaginatedListAsync(pageNumber, pageSize);

            Assert.Equal(1, page.CurrentPage);
            Assert.Equal(10, page.PageSize);
            Assert.Equal(10, page.Data.Count);
        }

        [Fact]
        public async Task ToPaginatedListAsync_PartialFirstPage_SkipsCountQuery()
        {
            await SeedAsync(4);

            var page = await _context.Items.OrderBy(i => i.Id).ToPaginatedListAsync(1, 10);

            Assert.Equal(4, page.TotalCount);
            Assert.DoesNotContain(_recorder.Commands, sql => sql.Contains("COUNT", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task ToPaginatedListAsync_FullFirstPage_RunsCountQuery()
        {
            await SeedAsync(12);

            var page = await _context.Items.OrderBy(i => i.Id).ToPaginatedListAsync(1, 10);

            Assert.Equal(12, page.TotalCount);
            Assert.Contains(_recorder.Commands, sql => sql.Contains("COUNT", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task ToPaginatedListAsync_NullSource_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => PaginatedListExtension.ToPaginatedListAsync<Item>(null!, 1, 10));
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }

        public class Item
        {
            public int Id { get; set; }
        }

        private class ItemsDbContext(DbContextOptions<ItemsDbContext> options) : DbContext(options)
        {
            public DbSet<Item> Items => Set<Item>();
        }

        private class CommandRecorder : DbCommandInterceptor
        {
            public List<string> Commands { get; } = [];

            public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
            {
                Commands.Add(command.CommandText);
                return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
            }

            public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
            {
                Commands.Add(command.CommandText);
                return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
            }
        }
    }
}
