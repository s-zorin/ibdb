using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Commands
{
    public class EditBookCommand : ICommand
    {
        public Guid? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
