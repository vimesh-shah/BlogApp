using BlogApp.Data;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.DataLoaders;

public class PostsByTagIdDataLoader : GroupedDataLoader<int, Post>
{
    private readonly IDbContextFactory<BlogDbContext> _dbContextFactory;

    public PostsByTagIdDataLoader(
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

        var posts = await dbContext.PostTags
                                        .AsNoTracking()
                                        .Include(x => x.Post)
                                        .Where(x => keys.Contains(x.TagId))
                                        .ToListAsync(cancellationToken);

        var lookup = posts.ToLookup(x => x.TagId, x => x.Post);

        return lookup;
    }
}
