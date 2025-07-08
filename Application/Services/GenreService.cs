using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;

namespace Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GenreDTO>> GetGenresAsync()
        {
            var genres = await _repository.GetAllAsync();
            return genres.Adapt<IEnumerable<GenreDTO>>();
        }

        public async Task<GenreDTO> GetGenreByIdAsync(int id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Genre with ID {id} not found");

            return genre.Adapt<GenreDTO>();
        }

        public async Task<GenreDTO> CreateGenreAsync(GenreDTO dto)
        {
            var genre = dto.Adapt<Genre>();
            var createdGenre = await _repository.CreateAsync(genre);
            return createdGenre.Adapt<GenreDTO>();
        }

        public async Task<GenreDTO> UpdateGenreAsync(GenreDTO dto)
        {
            var existingGenre = await _repository.GetByIdAsync(dto.Id);
            if (existingGenre == null)
                throw new KeyNotFoundException($"Genre with ID {dto.Id} not found");

            var genre = dto.Adapt<Genre>();
            var updatedGenre = await _repository.UpdateAsync(genre);
            return updatedGenre.Adapt<GenreDTO>();
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _repository.GetByIdAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Genre with ID {id} not found");

            await _repository.DeleteAsync(id);
        }
    }
}