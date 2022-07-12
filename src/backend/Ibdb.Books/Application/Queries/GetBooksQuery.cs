using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Queries
{
    public class GetBooksQuery : IQuery<(ICollection<BookDto> Books, int TotalCount)>
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
