using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide the genre name")]
        [MinLength(4)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
