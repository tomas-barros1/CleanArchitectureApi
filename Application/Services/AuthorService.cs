using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthorsAsync()
        {
            var genres = await _repository.GetAllAsync();
            return genres.Adapt<IEnumerable<AuthorDTO>>();
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Author with ID {id} not found");

            return genre.Adapt<AuthorDTO>();
        }

        public async Task<AuthorDTO> CreateAuthorAsync(AuthorDTO dto)
        {
            var genre = dto.Adapt<Author>();
            var createdAuthor = await _repository.CreateAsync(genre);
            return createdAuthor.Adapt<AuthorDTO>();
        }

        public async Task<AuthorDTO> UpdateAuthorAsync(AuthorDTO dto)
        {
            var existingAuthor = await _repository.GetByIdAsync(dto.Id);
            if (existingAuthor == null)
                throw new KeyNotFoundException($"Author with ID {dto.Id} not found");

            var genre = dto.Adapt<Author>();
            var updatedAuthor = await _repository.UpdateAsync(genre);
            return updatedAuthor.Adapt<AuthorDTO>();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Author with ID {id} not found");

            await _repository.DeleteAsync(id);
        }
    }
}
