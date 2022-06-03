namespace Ibdb.Books.Application.Dtos
{
    public class EditBookDto
    {
        public Guid? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
