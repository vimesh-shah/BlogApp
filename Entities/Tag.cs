using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Entities;

public class Tag : BaseEntity, IEntityTypeConfiguration<Tag>
{
    public string Name { get; set; }

    public IEnumerable<Post> Posts { get; set; }

    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .HasIndex()
            .IsUnique();

        builder
            .HasMany(t => t.Posts)
            .WithMany(p => p.Tags);
    }
}