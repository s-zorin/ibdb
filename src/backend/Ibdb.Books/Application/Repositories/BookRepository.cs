using Ibdb.Books.Domain;
using Ibdb.Books.Infrastructure.Ef;
using Ibdb.Shared.Application;
using Microsoft.EntityFrameworkCore;

namespace Ibdb.Books.Application.Repositories
{
    public class BookRepository : IRepository<Book>
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
    }
}
