using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetBooksAsync();
        Task<BookDTO> GetBookByIdAsync(int id);
        Task<BookDTO> CreateBookAsync(BookDTO dto);
        Task<BookDTO> UpdateBookAsync(BookDTO dto);
        Task DeleteBookAsync(int id);
    }
}
