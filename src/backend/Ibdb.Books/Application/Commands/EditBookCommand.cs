namespace Ibdb.Books.Application.Commands
{
    public record EditBookCommand(Guid Id, string Title, string? Description);
}
