using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Queries
{
    public class GetBookReviewsQuery : IQuery<ICollection<ReviewDto>>
    {
        public Guid BookId { get; set; }
    }
}
