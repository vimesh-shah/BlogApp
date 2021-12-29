using BlogApp.Data;
using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.DataLoaders;

public class TagByIdDataLoader : BatchDataLoader<int, Tag>
{
    private readonly IDbContextFactory<BlogDbContext> _dbContextFactory;

    public TagByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<BlogDbContext> dbContextFactory)
        : base(batchScheduler)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Tag>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var tags = await dbContext.Tags
                                    .AsNoTracking()
                                    .Where(x => keys.Contains(x.Id))
                                    .ToDictionaryAsync(x => x.Id, cancellationToken);

        return tags;
    }
}
