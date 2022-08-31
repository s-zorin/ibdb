using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> Find(Guid id);
    }
}
