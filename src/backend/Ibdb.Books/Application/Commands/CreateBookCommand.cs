using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Commands
{
    public class CreateBookCommand : ICommand
    {
        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
