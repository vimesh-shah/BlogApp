using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data;

public class BlogDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }
}
