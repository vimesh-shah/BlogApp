using BlogApp.Data;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.DataLoaders;

public class TagsByPostIdDataLoader : GroupedDataLoader<int, Tag>
{
    private readonly IDbContextFactory<BlogDbContext> _dbContextFactory;

    public TagsByPostIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<BlogDbContext> dbContextFactory,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<int, Tag>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var tags = await dbContext.PostTags
                                        .AsNoTracking()
                                        .Include(x => x.Tag)
                                        .Where(x => keys.Contains(x.PostId))
                                        .ToListAsync(cancellationToken);

        var lookup = tags.ToLookup(x => x.PostId, x => x.Tag);

        return lookup;
    }
}
