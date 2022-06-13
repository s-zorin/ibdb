namespace Ibdb.Books.Application.Dtos
{
    public class BookCreatedEventDataDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
