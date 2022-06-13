using Ibdb.Reviews.Domain;
using Ibdb.Reviews.Infrastructure.Ef;
using Ibdb.Shared.Application;
using Microsoft.EntityFrameworkCore;

namespace Ibdb.Reviews.Repositories
{
    public class ReviewRepository : IRepository<Review>
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
    }
}
