using BlogApp.Entities;
using BlogApp.Graphql.DataLoaders;

namespace BlogApp.Graphql.Types;

public class PostType : ObjectType<Post>
{
    protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(x => x.Id)
            .ResolveNode((ctx, id) => ctx.DataLoader<PostByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor
            .Field(x => x.Author)
            .Resolve((context) =>
            {
                var parent = context.Parent<Post>();
                return context.DataLoader<AuthorByIdDataLoader>().LoadAsync(parent.AuthorId, context.RequestAborted);
            });
    }
}
