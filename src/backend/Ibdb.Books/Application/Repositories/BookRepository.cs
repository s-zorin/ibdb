using Ibdb.Books.Domain;
using Ibdb.Books.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;

namespace Ibdb.Books.Application.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksContext _context;

        public BookRepository(BooksContext context)
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

        public async Task<int> Count()
        {
            return await _context.Books.CountAsync(b => !b.IsDeleted);
        }

        public async Task<ICollection<Book>> Get(int skip, int take)
        {
            return await _context.Books.Where(b => !b.IsDeleted).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Book?> Find(Guid id)
        {
            return await _context.Books.SingleOrDefaultAsync(b => !b.IsDeleted && b.Id == id);
        }
    }
}
