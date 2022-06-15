using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Application.Queries;
using Ibdb.Books.Application.Repositories;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
{
    public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, ICollection<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<BookDto>> Handle(GetBooksQuery query)
        {
            var books = await _bookRepository.Get(query.Skip, query.Take);
            return _mapper.Map<ICollection<BookDto>>(books);
        }
    }
}
