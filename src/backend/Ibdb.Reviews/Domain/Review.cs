namespace Ibdb.Reviews.Domain
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public string Text { get; set; } = string.Empty;

        public float Score { get; set; }
    }
}
