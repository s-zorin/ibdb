namespace Ibdb.Books.Domain
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public float Rating { get; set; }

        public bool IsDeleted { get; set; }

        public Dictionary<Guid, float> ReviewScores { get; set; } = new Dictionary<Guid, float>();
    }
}
