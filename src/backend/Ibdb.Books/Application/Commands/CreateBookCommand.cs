using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Commands
{
    public record CreateBookCommand(Guid Id, string Title, string? Description);
}
