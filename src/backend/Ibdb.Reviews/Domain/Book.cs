namespace Ibdb.Reviews.Domain
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
