using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAll()
        {
            var genres = await _service.GetGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetById(int id)
        {
            var genre = await _service.GetGenreByIdAsync(id);
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDTO>> Create([FromBody] GenreDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdGenre = await _service.CreateGenreAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdGenre.Id }, createdGenre);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreDTO>> Update(int id, [FromBody] GenreDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("ID in URL does not match ID in request body");

            var updatedGenre = await _service.UpdateGenreAsync(dto);
            return Ok(updatedGenre);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteGenreAsync(id);
            return NoContent();
        }
    }
}
