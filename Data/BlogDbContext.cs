using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data;

public class BlogDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = default!;

    public DbSet<Author> Authors { get; set; } = default!;

    public DbSet<Tag> Tags { get; set; } = default!;

    public DbSet<PostTag> PostTags { get; set; } = default!;

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }
}
