using System.ComponentModel.DataAnnotations;

namespace Ibdb.Books.Application.Dtos
{
    public class GetBooksDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public int Skip { get; set; }

        [Required]
        [Range(1, 100)]
        public int Take { get; set; }
    }
}
