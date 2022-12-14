using Ibdb.Books.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        public Task<ICollection<Book>> Get(int skip, int take);

        public Task<Book?> Find(Guid id);

        public Task<int> Count();
    }
}
