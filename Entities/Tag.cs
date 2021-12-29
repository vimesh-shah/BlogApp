using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = default!;

    [NotMapped]
    public IEnumerable<Post> Posts { get; set; } = default!;

    public IEnumerable<PostTag> PostTags { get; set; } = default!;
}