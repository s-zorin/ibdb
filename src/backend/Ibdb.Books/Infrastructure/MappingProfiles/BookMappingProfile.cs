using AutoMapper;
using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Domain;

namespace Ibdb.Books.Infrastructure.MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
