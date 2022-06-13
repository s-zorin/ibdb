namespace Ibdb.Reviews.Application.Dtos
{
    public class CreateReviewDto
    {
        public Guid BookId { get; set; }

        public string? Text { get; set; }

        public float Score { get; set; }
    }
}
