using Application.DTOs;

namespace Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetGenresAsync();
        Task<GenreDTO> GetGenreByIdAsync(int id);
        Task<GenreDTO> CreateGenreAsync(GenreDTO dto);
        Task<GenreDTO> UpdateGenreAsync(GenreDTO dto);
        Task DeleteGenreAsync(int id);
    }
}
