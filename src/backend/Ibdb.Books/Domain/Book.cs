namespace Ibdb.Books.Domain
{
    public class Book
    {
        public Book(Guid id, string title, string? description, float rating)
        {
            Id = id;
            Title = title;
            Description = description;
            Rating = rating;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public float Rating { get; set; }
    }
}
