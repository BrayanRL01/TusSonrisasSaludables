using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public GenresController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<GenresModel>
        [HttpGet("Genres")]
        public async Task<ActionResult<IEnumerable<VwGenre>>> SP_GetGenresView()
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }

            try
            {
                var genres = await _context.VwGenres.FromSqlRaw("EXEC SP_GetGenresView").ToListAsync();
                return Ok(genres);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<GenresModel>/5
        [HttpGet("Genre/{id}")]
        public async Task<ActionResult> SP_GetGenreView(int id)
        {
            try
            {
                var genres = await _context.VwGenres.FromSqlInterpolated($"EXEC SP_GetGenreView {id}").ToListAsync();
                var genre = genres.FirstOrDefault();

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado el género: " + ex.Message);
            }
        }

        // POST api/<GenresModel>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GenresModel>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GenresModel>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
