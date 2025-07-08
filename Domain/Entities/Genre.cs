using Mapster;

namespace Domain.Entities
{
    [AdaptTo("[name]DTO"), GenerateMapper]
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
