using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide the book title")]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide the genre id")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Please provide the author id")]
        public int AuthorId { get; set; }
    }
}
