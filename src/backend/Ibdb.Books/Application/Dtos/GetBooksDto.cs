using System.ComponentModel.DataAnnotations;

namespace Ibdb.Books.Application.Dtos
{
    public record GetBooksDto(int Skip, int Take);
}
