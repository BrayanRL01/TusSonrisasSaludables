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
        private readonly TusSonrisasSaludablesContext context;

        public IdentificationsController(TusSonrisasSaludablesContext _context)
        {
            context = _context;
        }

        // GET: api/<IdentificationsModel>
        [HttpGet("GetIDTypesView")]
        public async Task<ActionResult<IEnumerable<VwIdentification>>> GetProvinces()
        {
            if (context.VwIdentifications == null)
            {
                return NotFound();
            }

            try
            {
                var identifications = await context.VwIdentifications.FromSqlRaw("EXEC SP_GetIdentificationsView").ToListAsync();
                return Ok(identifications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<IdentificationsModel>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
