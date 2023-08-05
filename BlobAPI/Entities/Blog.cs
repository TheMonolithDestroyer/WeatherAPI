namespace BlobAPI.Entities
{
    public class Blog
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Header { get; set; } = default!;
        public string Content { get; set; } = default!;
    }
}
