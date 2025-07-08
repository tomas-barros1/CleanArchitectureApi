using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksAsync()
        {
            var genres = await _repository.GetAllAsync();
            return genres.Adapt<IEnumerable<BookDTO>>();
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            return genre.Adapt<BookDTO>();
        }

        public async Task<BookDTO> CreateBookAsync(BookDTO dto)
        {
            var genre = dto.Adapt<Book>();
            var createdBook = await _repository.CreateAsync(genre);
            return createdBook.Adapt<BookDTO>();
        }

        public async Task<BookDTO> UpdateBookAsync(BookDTO dto)
        {
            var existingBook = await _repository.GetByIdAsync(dto.Id);
            if (existingBook == null)
                throw new KeyNotFoundException($"Book with ID {dto.Id} not found");

            var genre = dto.Adapt<Book>();
            var updatedBook = await _repository.UpdateAsync(genre);
            return updatedBook.Adapt<BookDTO>();
        }

        public async Task DeleteBookAsync(int id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            await _repository.DeleteAsync(id);
        }
    }
}
