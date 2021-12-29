using BlogApp.Data;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.DataLoaders;

public class PostByIdDataLoader : BatchDataLoader<int, Post>
{
    private readonly IDbContextFactory<BlogDbContext> _dbContextFactory;

    public PostByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<BlogDbContext> dbContextFactory)
        : base(batchScheduler)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Post>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var posts = await dbContext.Posts
                                        .AsNoTracking()
                                        .Where(x => keys.Contains(x.Id))
                                        .ToDictionaryAsync(x => x.Id, cancellationToken);

        return posts;
    }
}
