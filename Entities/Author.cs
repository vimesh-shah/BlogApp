namespace BlogApp.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; } = default!;

    public IEnumerable<Post> Posts { get; set; } = default!;
}
