using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o título do livro")]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o id do gênero")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Informe o id do autor")]
        public int AuthorId { get; set; }
    }
}
