using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public BrandsController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<BrandsController>
        [HttpGet("GetBrandsView")]
        public async Task<ActionResult<IEnumerable<VwBrand>>> SP_GetBrandsView()
        {
            if (_context.VwBrands == null)
            {
                return NotFound();
            }

            try
            {
                var brands = await _context.VwBrands.FromSqlRaw("EXEC SP_GetBrandsView").ToListAsync();
                return brands;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<BrandsController>/5
        [HttpGet("GetBrandView/{id}")]
        public async Task<ActionResult> SP_GetBrandView(int id)
        {
            if (_context.VwBrands == null)
            {
                return NotFound();
            }

            try
            {
                var brands = await _context.VwBrands.FromSqlInterpolated($"EXEC SP_GetBrandView {id}").ToListAsync();
                var brand = brands.FirstOrDefault();
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // POST api/<BrandsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
