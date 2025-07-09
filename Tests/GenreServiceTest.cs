using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests;

public class GenreServiceTests
{
    private readonly Mock<IGenreRepository> _repositoryMock;
    private readonly GenreService _service;

    public GenreServiceTests()
    {
        _repositoryMock = new Mock<IGenreRepository>();
        _service = new GenreService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetGenresAsync_ReturnsListOfGenreDTO()
    {
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Genre 1" },
            new Genre { Id = 2, Name = "Genre 2" }
        };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(genres);

        var result = await _service.GetGenresAsync();

        Assert.NotNull(result);
        Assert.Collection(result,
            item => Assert.Equal("Genre 1", item.Name),
            item => Assert.Equal("Genre 2", item.Name));
    }

    [Fact]
    public async Task GetGenreByIdAsync_ExistingId_ReturnsGenreDTO()
    {
        var genre = new Genre { Id = 1, Name = "Genre 1" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(genre);

        var result = await _service.GetGenreByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Genre 1", result.Name);
    }

    [Fact]
    public async Task GetGenreByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Genre)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetGenreByIdAsync(99));
    }

    [Fact]
    public async Task CreateGenreAsync_ReturnsCreatedGenreDTO()
    {
        var genreDto = new GenreDTO { Name = "New Genre" };
        var genre = new Genre { Id = 1, Name = "New Genre" };

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Genre>())).ReturnsAsync(genre);

        var result = await _service.CreateGenreAsync(genreDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Genre", result.Name);
    }

    [Fact]
    public async Task UpdateGenreAsync_ExistingId_ReturnsUpdatedGenreDTO()
    {
        var genreDto = new GenreDTO { Id = 1, Name = "Updated Genre" };
        var existingGenre = new Genre { Id = 1, Name = "Old Genre" };
        var updatedGenre = new Genre { Id = 1, Name = "Updated Genre" };

        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingGenre);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Genre>())).ReturnsAsync(updatedGenre);

        var result = await _service.UpdateGenreAsync(genreDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Updated Genre", result.Name);
    }

    [Fact]
    public async Task UpdateGenreAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        var genreDto = new GenreDTO { Id = 99, Name = "Genre" };
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Genre)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateGenreAsync(genreDto));
    }

    [Fact]
    public async Task DeleteGenreAsync_ExistingId_DeletesSuccessfully()
    {
        var existingGenre = new Genre { Id = 1, Name = "Genre" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingGenre);
        _repositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        await _service.DeleteGenreAsync(1);

        _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteGenreAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Genre)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteGenreAsync(99));
    }
}
