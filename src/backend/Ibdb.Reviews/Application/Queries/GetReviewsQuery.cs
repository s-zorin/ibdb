using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Queries
{
    public class GetReviewsQuery : IQuery<(ICollection<ReviewDto> Reviews, int TotalCount)>
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
