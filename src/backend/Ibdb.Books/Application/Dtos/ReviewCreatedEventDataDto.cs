namespace Ibdb.Books.Application.Dtos
{
    public class ReviewCreatedEventDataDto
    {
        public Guid BookId { get; set; }

        public string Text { get; set; } = string.Empty;

        public float Score { get; set; }
    }
}
