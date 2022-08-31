using Ibdb.Reviews.Domain;
using Ibdb.Reviews.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;

namespace Ibdb.Reviews.Application.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ReviewsContext _context;

        public BookRepository(ReviewsContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdate(Book entity)
        {
            if (await _context.Books.AnyAsync(x => x.Id == entity.Id))
            {
                _context.Attach(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Attach(entity).State = EntityState.Added;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Book?> Find(Guid id)
        {
            return await _context.Books.SingleOrDefaultAsync(b => !b.IsDeleted && b.Id == id);
        }
    }
}
