using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentificationsController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public IdentificationsController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<IdentificationsModel>
        [HttpGet("GetIDTypesView")]
        public async Task<ActionResult<IEnumerable<VwIdentification>>> GetProvinces()
        {
            if (_context.VwIdentifications == null)
            {
                return NotFound();
            }

            try
            {
                var identifications = await _context.VwIdentifications.FromSqlRaw("EXEC SP_GetIdentificationsView").ToListAsync();
                return Ok(identifications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<IdentificationsModel>/5
        [HttpGet("GetIDTypeView/{id}")]
        public async Task<ActionResult> SP_GetIDTypeView(int id)
        {
            try
            {
                var types = await _context.VwProvinces.FromSqlInterpolated($"EXEC SP_GetIdentificationView {id}").ToListAsync();
                var type = types.FirstOrDefault();

                return Ok(type);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado el tipo de identificación: " + ex.Message);
            }
        }

        // POST api/<IdentificationsModel>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<IdentificationsModel>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IdentificationsModel>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
