using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> GetAuthorsAsync();
        Task<AuthorDTO> GetAuthorByIdAsync(int id);
        Task<AuthorDTO> CreateAuthorAsync(AuthorDTO dto);
        Task<AuthorDTO> UpdateAuthorAsync(AuthorDTO dto);
        Task DeleteAuthorAsync(int id);
    }
}
