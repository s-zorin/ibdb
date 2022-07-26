using Ibdb.Books.Application.Dtos;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Queries
{
    public class FindBookQuery : IQuery<BookDto?>
    {
        public Guid Id { get; set; }
    }
}
