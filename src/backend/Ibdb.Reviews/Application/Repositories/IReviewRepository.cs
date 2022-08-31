using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<ICollection<Review>> Get(int skip, int take);

        Task<Review?> Find(Guid id);

        Task<int> Count();

        Task<ICollection<Review>> GetByBookId(Guid bookId);
    }
}
