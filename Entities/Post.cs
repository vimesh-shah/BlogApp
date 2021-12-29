using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Entities;

public class Post : BaseEntity, IEntityTypeConfiguration<Post>
{
    public string Title { get; set; }

    public string Content { get; set; }

    public Author Author { get; set; }

    public int AuthorId { get; set; }

    public IEnumerable<Tag> Tags { get; set; }

    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasOne(p => p.Author)
            .WithMany(a => a.Posts)
            .HasForeignKey(p => p.AuthorId);

        builder
            .HasMany(p => p.Tags)
            .WithMany(t => t.Posts);
    }
}
