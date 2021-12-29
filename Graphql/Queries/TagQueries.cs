using BlogApp.Data;
using BlogApp.Entities;
using BlogApp.Graphql.DataLoaders;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class TagQueries
{
    public async Task<List<Tag>> GetTags(
        BlogDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.Tags.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Tag> GetTag(
        [ID(nameof(Tag))] int id,
        TagByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(id, cancellationToken);
    }
}