using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Entities;
using BackEnd.Models;
using Microsoft.Data.SqlClient;

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
                await _context.Database.CloseConnectionAsync();

                return Ok(provinces);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Provinces/5
        [HttpGet("GetProvinceView/{id}")]
        public async Task<ActionResult> SP_GetProvinceView(int id)
        {
            try
            {
                var provinces = await _context.VwProvinces.FromSqlInterpolated($"EXEC SP_GetProvinceView {id}").ToListAsync();
                var province = provinces.FirstOrDefault();
                await _context.Database.CloseConnectionAsync();

                return Ok(province);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la provincia: " + ex.Message);
            }
        }

        //// PUT: api/Provinces/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("PutProvince")]
        //public async Task<ActionResult<Province>> PutProvince([FromBody] ProvinceModel entity)
        //{
        //    try
        //    {
        //        string Query = "EXEC SP_EditProvince @ProvinceID, @ProvinceName";

        //        var param = new SqlParameter[]
        //        {
        //             new SqlParameter()
        //            {
        //                ParameterName = "@ProvinceID",
        //                SqlDbType = System.Data.SqlDbType.Int,
        //                Direction = System.Data.ParameterDirection.Input,
        //                Value = entity.ProvinceId
        //            },
        //            new SqlParameter()
        //            {
        //                ParameterName = "@ProvinceName",
        //                SqlDbType = System.Data.SqlDbType.VarChar,
        //                Direction = System.Data.ParameterDirection.Input,
        //                Value = entity.ProvinceName
        //            }
        //        };

        //        await _context.Database.ExecuteSqlRawAsync(Query, param);
        //        await _context.SaveChangesAsync();

        //        return Ok(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "No se pudo editar la provincia: " + ex.Message);
        //    }
        //}

        //// POST: api/Provinces
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("PostProvince")]
        //public async Task<ActionResult<Province>> PostProvince([FromBody] ProvinceModel entity)
        //{
        //    try
        //    {
        //        string Query = "EXEC SP_CreateProvince @ProvinceName";

        //        var param = new SqlParameter[]
        //        {
        //            new SqlParameter()
        //            {
        //                ParameterName = "@ProvinceName",
        //                SqlDbType = System.Data.SqlDbType.VarChar,
        //                Direction = System.Data.ParameterDirection.Input,
        //                Value = entity.ProvinceName
        //            }
        //        };

        //        await _context.Database.ExecuteSqlRawAsync(Query, param);
        //        await _context.SaveChangesAsync();

        //        return Ok(entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "No se pudo crear la provincia: " + ex.Message);
        //    }
        //}

        //// DELETE: api/Provinces/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteProvince(int id)
        //{
        //    try
        //    {
        //        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteProvince {id}");
        //        await _context.SaveChangesAsync();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "No se pudo eliminar la provincia: " + ex.Message);
        //    }
        //}

        //private bool ProvinceExists(int id)
        //{
        //    return (_context.Provinces?.Any(e => e.ProvinceId == id)).GetValueOrDefault();
        //}
    }
}
