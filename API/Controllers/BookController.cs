using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            var genres = await _service.GetBooksAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            var genre = await _service.GetBookByIdAsync(id);
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Create([FromBody] BookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBook = await _service.CreateBookAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> Update(int id, [FromBody] BookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("ID in URL does not match ID in request body");

            var updatedBook = await _service.UpdateBookAsync(dto);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
