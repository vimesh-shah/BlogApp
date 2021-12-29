using BlogApp.Data;
using BlogApp.Entities;
using BlogApp.Graphql.DataLoaders;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Graphql.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class PostQueries
{
    public async Task<IEnumerable<Post>> GetPosts(
        BlogDbContext dbContext)
    {
        return await dbContext.Posts.ToListAsync();
    }

    public async Task<Post> GetPost(
        [ID(nameof(Post))] int id,
        PostByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        return await dataLoader.LoadAsync(id, cancellationToken);
    }
}
