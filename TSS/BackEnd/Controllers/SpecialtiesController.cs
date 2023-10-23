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
    public class SpecialtiesController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public SpecialtiesController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<SpecialtiesController>
        [HttpGet("Specialties")]
        public async Task<ActionResult<IEnumerable<VwSpecialty>>> SP_GetSpecialtiesView()
        {
            if (_context.VwSpecialties == null)
            {
                return NotFound();
            }

            try
            {
                var specialties = await _context.VwSpecialties.FromSqlRaw("EXEC SP_GetSpecialtiesView").ToListAsync();
                return Ok(specialties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET api/<SpecialtiesController>/5
        [HttpGet("Specialty/{id}")]
        public async Task<ActionResult> SP_GetSpecialty(int id)
        {
            try
            {
                var users = await _context.VwSpecialties.FromSqlInterpolated($"EXEC SP_GetSpecialtyView {id}").ToListAsync();
                var user = users.FirstOrDefault();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la especialidad " + ex.Message);
            }
        }

        // POST api/<SpecialtiesController>
        [HttpPost("Specialty")]
        public async Task<ActionResult<Specialty>> PostSpecialty([FromBody] SpecialtyModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateSpecialty @SpecialtyName";

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@SpecialtyName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyName
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear la especialidad: " + ex.Message);
            }
        }

        // PUT api/<SpecialtiesController>/5
        [HttpPut("Specialty")]

        public async Task<ActionResult<Specialty>> PutSpecialty([FromBody] SpecialtyModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditSpecialty @ID, @SpecialtyName";

                var param = new SqlParameter[]
               {
                    new SqlParameter()
                    {

                        ParameterName = "@ID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyId
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@SpecialtyName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyName
                    }
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar la especialidad: " + ex.Message);
            }
        }

        #region DeleteAppointments
        // DELETE api/<SpecialtiesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpecialty(int id)
        {
            {
                try
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteSpecialty {id}");
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "No se pudo eliminar la especialidad: " + ex.Message);
                }
            }
        }
        #endregion
    }
}