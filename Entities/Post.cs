using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;

    public Author Author { get; set; } = default!;

    public int AuthorId { get; set; } = default!;

    [NotMapped]
    public IEnumerable<Tag> Tags { get; set; } = default!;

    public IEnumerable<PostTag> PostTags { get; set; } = default!;
}
