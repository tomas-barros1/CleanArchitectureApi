using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock;
    private readonly AuthorService _service;

    public AuthorServiceTests()
    {
        _repositoryMock = new Mock<IAuthorRepository>();
        _service = new AuthorService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAuthorsAsync_ReturnsListOfAuthorDTO()
    {
        // Arrange
        var authors = new List<Author>
        {
            new Author { Id = 1, Name = "Author 1" },
            new Author { Id = 2, Name = "Author 2" }
        };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(authors);

        // Act
        var result = await _service.GetAuthorsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Collection(result,
            item => Assert.Equal("Author 1", item.Name),
            item => Assert.Equal("Author 2", item.Name));
    }

    [Fact]
    public async Task GetAuthorByIdAsync_ExistingId_ReturnsAuthorDTO()
    {
        // Arrange
        var author = new Author { Id = 1, Name = "Author 1" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(author);

        // Act
        var result = await _service.GetAuthorByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Author 1", result.Name);
    }

    [Fact]
    public async Task GetAuthorByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Author)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetAuthorByIdAsync(99));
    }

    [Fact]
    public async Task CreateAuthorAsync_ReturnsCreatedAuthorDTO()
    {
        // Arrange
        var authorDto = new AuthorDTO { Name = "New Author" };
        var author = new Author { Id = 1, Name = "New Author" };

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Author>())).ReturnsAsync(author);

        // Act
        var result = await _service.CreateAuthorAsync(authorDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Author", result.Name);
    }

    [Fact]
    public async Task UpdateAuthorAsync_ExistingId_ReturnsUpdatedAuthorDTO()
    {
        // Arrange
        var authorDto = new AuthorDTO { Id = 1, Name = "Updated Author" };
        var existingAuthor = new Author { Id = 1, Name = "Author Old" };
        var updatedAuthor = new Author { Id = 1, Name = "Updated Author" };

        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingAuthor);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Author>())).ReturnsAsync(updatedAuthor);

        // Act
        var result = await _service.UpdateAuthorAsync(authorDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Updated Author", result.Name);
    }

    [Fact]
    public async Task UpdateAuthorAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var authorDto = new AuthorDTO { Id = 99, Name = "Author" };
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Author)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAuthorAsync(authorDto));
    }

    [Fact]
    public async Task DeleteAuthorAsync_ExistingId_DeletesSuccessfully()
    {
        // Arrange
        var existingAuthor = new Author { Id = 1, Name = "Author" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingAuthor);
        _repositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAuthorAsync(1);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteAuthorAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Author)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAuthorAsync(99));
    }
}
