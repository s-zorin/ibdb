using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Queries
{
    public class FindReviewQuery : IQuery<ReviewDto?>
    {
        public Guid Id { get; set; }
    }
}
