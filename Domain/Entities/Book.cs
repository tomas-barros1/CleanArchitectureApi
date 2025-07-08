using Mapster;

namespace Domain.Entities
{
    [AdaptTo("[name]DTO"), GenerateMapper]
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
    }
}
