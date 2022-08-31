using Ibdb.Reviews.Domain;
using Ibdb.Reviews.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;

namespace Ibdb.Reviews.Application.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewsContext _context;

        public ReviewRepository(ReviewsContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdate(Review entity)
        {
            if (await _context.Reviews.AnyAsync(x => x.Id == entity.Id))
            {
                _context.Attach(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Attach(entity).State = EntityState.Added;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> Count()
        {
            return await _context.Reviews.CountAsync(r => !r.IsDeleted);
        }

        public async Task<Review?> Find(Guid id)
        {
            return await _context.Reviews.SingleOrDefaultAsync(r => !r.IsDeleted && r.Id == id);
        }

        public async Task<ICollection<Review>> Get(int skip, int take)
        {
            return await _context.Reviews.Where(r => !r.IsDeleted).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<ICollection<Review>> GetByBookId(Guid bookId)
        {
            return await _context.Reviews.Where(r => !r.IsDeleted && r.BookId == bookId).ToListAsync();
        }
    }
}
