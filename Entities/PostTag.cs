namespace BlogApp.Entities
{
    public class PostTag
    {
        public int Id { get; set; } = default!;

        public int PostId { get; set; } = default!;

        public Post Post { get; set; } = default!;

        public int TagId { get; set; } = default!;

        public Tag Tag { get; set; } = default!;
    }
}