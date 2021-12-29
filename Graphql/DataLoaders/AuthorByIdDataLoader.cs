using BlogApp.Data;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.DataLoaders;

public class AuthorByIdDataLoader : BatchDataLoader<int, Author>
{
    private readonly IDbContextFactory<BlogDbContext> _dbContextFactory;

    public AuthorByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<BlogDbContext> dbContextFactory)
        : base(batchScheduler)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Author>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var authors = await dbContext.Authors
                                        .AsNoTracking()
                                        .Where(x => keys.Contains(x.Id))
                                        .ToDictionaryAsync(x => x.Id, cancellationToken);

        return authors;
    }
}
