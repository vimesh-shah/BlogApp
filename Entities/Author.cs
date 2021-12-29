using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Entities;

public class Author : BaseEntity, IEntityTypeConfiguration<Author>
{
    public string Name { get; set; }

    public IEnumerable<Post> Posts { get; set; }

    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .HasMany(a => a.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId);
    }
}
