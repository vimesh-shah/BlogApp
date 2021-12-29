using BlogApp.Data;
using BlogApp.Entities;
using BlogApp.Graphql.DataLoaders;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class AuthorQueries
{
    public async Task<IEnumerable<Author>> GetAuthors(
        BlogDbContext dbContext)
    {
        return await dbContext.Authors.ToListAsync();
    }

    public async Task<Author> GetAuthor(
        [ID(nameof(Author))] int id,
        AuthorByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(id, cancellationToken);
    }
}
