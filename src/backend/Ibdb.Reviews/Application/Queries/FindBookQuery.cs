using Ibdb.Reviews.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Queries
{
    public class FindBookQuery : IQuery<BookDto?>
    {
        public Guid BookId { get; set; }
    }
}
