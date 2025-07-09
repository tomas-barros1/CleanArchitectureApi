using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _repositoryMock;
    private readonly BookService _service;

    public BookServiceTests()
    {
        _repositoryMock = new Mock<IBookRepository>();
        _service = new BookService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetBooksAsync_ReturnsListOfBookDTO()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1" },
            new Book { Id = 2, Title = "Book 2" }
        };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

        var result = await _service.GetBooksAsync();

        Assert.NotNull(result);
        Assert.Collection(result,
            item => Assert.Equal("Book 1", item.Title),
            item => Assert.Equal("Book 2", item.Title));
    }

    [Fact]
    public async Task GetBookByIdAsync_ExistingId_ReturnsBookDTO()
    {
        var book = new Book { Id = 1, Title = "Book 1" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

        var result = await _service.GetBookByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Book 1", result.Title);
    }

    [Fact]
    public async Task GetBookByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetBookByIdAsync(99));
    }

    [Fact]
    public async Task CreateBookAsync_ReturnsCreatedBookDTO()
    {
        var bookDto = new BookDTO { Title = "New Book" };
        var book = new Book { Id = 1, Title = "New Book" };

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Book>())).ReturnsAsync(book);

        var result = await _service.CreateBookAsync(bookDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Book", result.Title);
    }

    [Fact]
    public async Task UpdateBookAsync_ExistingId_ReturnsUpdatedBookDTO()
    {
        var bookDto = new BookDTO { Id = 1, Title = "Updated Book" };
        var existingBook = new Book { Id = 1, Title = "Old Book" };
        var updatedBook = new Book { Id = 1, Title = "Updated Book" };

        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBook);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(updatedBook);

        var result = await _service.UpdateBookAsync(bookDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Updated Book", result.Title);
    }

    [Fact]
    public async Task UpdateBookAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        var bookDto = new BookDTO { Id = 99, Title = "Book" };
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateBookAsync(bookDto));
    }

    [Fact]
    public async Task DeleteBookAsync_ExistingId_DeletesSuccessfully()
    {
        var existingBook = new Book { Id = 1, Title = "Book" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBook);
        _repositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        await _service.DeleteBookAsync(1);

        _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteBookAsync_NonExistingId_ThrowsKeyNotFoundException()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteBookAsync(99));
    }
}