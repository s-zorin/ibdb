using AutoMapper;
using Ibdb.Books.Application.Commands;
using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Application.Queries;
using Ibdb.Books.Domain;

namespace Ibdb.Books.Infrastructure.MappingProfiles
{
    public class BooksMappingProfile : Profile
    {
        public BooksMappingProfile()
        {
            CreateMap<Book, BookDto>();

            CreateMap<CreateBookDto, CreateBookCommand>();

            CreateMap<CreateBookCommand, BookCreatedEventDataDto>();

            CreateMap<EditBookDto, EditBookCommand>();

            CreateMap<DeleteBookDto, DeleteBookCommand>();

            CreateMap<EditBookCommand, BookEditedEventDataDto>();

            CreateMap<DeleteBookCommand, BookDeletedEventDataDto>();

            CreateMap<GetBooksDto, GetBooksQuery>();
        }
    }
}
