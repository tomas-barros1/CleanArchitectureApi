using Mapster;

namespace Domain.Entities
{
    [AdaptTo("[name]DTO"), GenerateMapper]
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
