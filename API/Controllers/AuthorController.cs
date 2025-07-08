using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAll()
        {
            var genres = await _service.GetAuthorsAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetById(int id)
        {
            var genre = await _service.GetAuthorByIdAsync(id);
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Create([FromBody] AuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAuthor = await _service.CreateAuthorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDTO>> Update(int id, [FromBody] AuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("ID in URL does not match ID in request body");

            var updatedAuthor = await _service.UpdateAuthorAsync(dto);
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
