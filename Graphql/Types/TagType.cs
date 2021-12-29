using BlogApp.Entities;
using BlogApp.Graphql.DataLoaders;

namespace BlogApp.Graphql.Types;

public class TagType : ObjectType<Tag>
{
    protected override void Configure(IObjectTypeDescriptor<Tag> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(x => x.Id)
            .ResolveNode((context, id) => context.DataLoader<TagByIdDataLoader>().LoadAsync(id, context.RequestAborted));

        descriptor
            .Field(x => x.Posts)
            .Resolve((context) =>
            {
                var parent = context.Parent<Tag>();
                return context.DataLoader<PostsByTagIdDataLoader>().LoadAsync(parent.Id, context.RequestAborted);
            });

        descriptor
            .Field(x => x.PostTags)
            .Ignore();
    }
}
