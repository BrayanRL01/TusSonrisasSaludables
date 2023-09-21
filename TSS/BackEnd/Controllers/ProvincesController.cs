using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Entities;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public ProvincesController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/Provinces
        [HttpGet("GetProvincesView")]
        public async Task<ActionResult<IEnumerable<VwProvince>>> GetProvinces()
        {
            if (_context.VwProvinces == null)
            {
                return NotFound();
            }

            try
            {
                var provinces = await _context.VwProvinces.FromSqlRaw("EXEC SP_GetProvincesView").ToListAsync();
                return Ok(provinces);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Provinces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
            if (_context.Provinces == null)
            {
                return NotFound();
            }
            var province = await _context.Provinces.FindAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }

        // PUT: api/Provinces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvince(int id, Province province)
        {
            if (id != province.ProvinceId)
            {
                return BadRequest();
            }

            _context.Entry(province).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Provinces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Province>> PostProvince(Province province)
        {
            if (_context.Provinces == null)
            {
                return Problem("Entity set 'TusSonrisasSaludablesContext.Provinces'  is null.");
            }
            _context.Provinces.Add(province);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvince", new { id = province.ProvinceId }, province);
        }

        // DELETE: api/Provinces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            if (_context.Provinces == null)
            {
                return NotFound();
            }
            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvinceExists(int id)
        {
            return (_context.Provinces?.Any(e => e.ProvinceId == id)).GetValueOrDefault();
        }
    }
}
