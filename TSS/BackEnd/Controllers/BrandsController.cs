using BackEnd.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<VwBrand>>> SP_GetBrandsView()
        {
            if (_context.VwBrands == null)
            {
                return NotFound();
            }

            try
            {
                var brands = await _context.VwBrands.FromSqlRaw("EXEC SP_GetBrandsView").ToListAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok(brands);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<BrandsController>/5
        [HttpGet("Brand/{id}")]
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
                await _context.Database.CloseConnectionAsync();

                return Ok(brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // POST api/<BrandsController>
        [HttpPost("Brand")]
        public async Task<ActionResult<Brand>> PostBrand([FromBody] BrandModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateBrand @BrandName";

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@BrandName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BrandName
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();


                return Ok("Marca creada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo agregar la marca: " + ex.Message);
            }
        }

        // PUT api/<BrandsController>/5
        [HttpPut("Brand")]
        public async Task<ActionResult<Brand>> PutBrand([FromBody] BrandModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditBrand @BrandID, @BrandName";

                var param = new SqlParameter[]
                {
                     new SqlParameter()
                    {
                        ParameterName = "@BrandID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BrandId
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@BrandName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BrandName
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();


                return Ok("Marca actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo modificar la marca: " + ex.Message);
            }
        }
        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteBrand {id}");
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Marca eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar la marca: " + ex.Message);
            }
        }
    }
}
