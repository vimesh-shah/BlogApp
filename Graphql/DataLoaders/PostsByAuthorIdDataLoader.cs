using BlogApp.Data;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.DataLoaders;

public class PostsByAuthorIdDataLoader : GroupedDataLoader<int, Post>
{
    private readonly IDbContextFactory<BlogDbContext> _dbContextFactory;

    public PostsByAuthorIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<BlogDbContext> dbContextFactory,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<int, Post>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
         CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var authors = await dbContext.Posts
                                        .AsNoTracking()
                                        .Where(x => keys.Contains(x.AuthorId))
                                        .ToListAsync(cancellationToken);

        var lookup = authors.ToLookup(x => x.AuthorId, x => x);

        return lookup;
    }
}
