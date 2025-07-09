using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name can't be null")]
        [MinLength(3)]
        [MaxLength(100)]

        public string Name { get; set; }
    }
}
