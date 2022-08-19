using Ibdb.Reviews.Domain;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        public Task<ICollection<Review>> Get(int skip, int take);

        public Task<Review?> Find(Guid id);

        public Task<int> Count();

        Task<ICollection<Review>> GetByBookId(Guid bookId);

        Task<Book?> FindBook(Guid id);

        Task<Review?> FindReview(Guid id);
    }
}
