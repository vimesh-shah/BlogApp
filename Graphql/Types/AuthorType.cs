using BlogApp.Entities;
using BlogApp.Graphql.DataLoaders;

namespace BlogApp.Graphql.Types;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(x => x.Id)
            .ResolveNode((ctx, id) => ctx.DataLoader<AuthorByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor
            .Field(x => x.Posts)
            .Resolve((context) =>
            {
                var parent = context.Parent<Author>();
                return context.DataLoader<PostsByAuthorIdDataLoader>().LoadAsync(parent.Id, context.RequestAborted);
            });
    }
}
