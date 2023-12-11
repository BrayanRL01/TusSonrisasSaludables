using BackEnd.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendationsController : ControllerBase
    {

        private readonly TusSonrisasSaludablesContext _context;

        public RecomendationsController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<AppointmentsController>
        [HttpGet("Recomendations")]
        public async Task<ActionResult<IEnumerable<VwRecomendation>>> SP_GetRecoemndacionesView()
        {
            if (_context.VwRecomendations == null)
            {
                return NotFound();
            }

            try
            {
                var recomendations = await _context.VwRecomendations.FromSqlRaw("EXEC SP_GetRecomendations").ToListAsync();
                return Ok(recomendations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Recomendation/{id}")]
        public async Task<ActionResult> SP_GetRecomendation(int id)
        {
            try
            {
                var recomendations = await _context.Recomendations.FromSqlInterpolated($"EXEC SP_GetRecomendation {id}").ToListAsync();
                var recomendation = recomendations.FirstOrDefault();

                return Ok(recomendation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado el usuario: " + ex.Message);
            }
        }

        [HttpPost("Recomendation")]
        public async Task<ActionResult<Recomendation>> PostRecomendations([FromBody] RecomendationModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateRecomendation @Doctor, @Specialty, @Info, @Image";

                var param = new SqlParameter[]
               {

                    new SqlParameter()
                    {
                        ParameterName = "@Doctor",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Specialty",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Info",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Information
                    },
                     new SqlParameter()
                     {
                        ParameterName = "@Image",
                        SqlDbType = System.Data.SqlDbType.Image,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.PostImage
                     }
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok($"Recomendacion creada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se creó la recomendacion: " + ex.Message);
            }
        }


        [HttpPut("Recomendation")]
        public async Task<ActionResult<Recomendation>> PutRecomendations([FromBody] RecomendationModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditRecomendation @ID, @Specialty, @Info";

                var param = new SqlParameter[]
               {

                    new SqlParameter()
                    {
                        ParameterName = "@ID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.RecomendationId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Specialty",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Info",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Information
                    },
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok($"Recomendacion editada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se editó la recomendacion: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecomendation(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteRecomendation {id}");
                await _context.SaveChangesAsync();
                return Ok("Recomendación eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar la recomendación: " + ex.Message);
            }
        }

        [HttpPut("ChangeImage")]
        public async Task<ActionResult<Recomendation>> ChangeImage([FromBody] RecomendationModel entity)
        {
            try
            {
                string Query = "EXEC SP_ChangeRecomendationImage @ID, @Image";

                var param = new SqlParameter[]
               {

                    new SqlParameter()
                    {
                        ParameterName = "@ID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.RecomendationId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Image",
                        SqlDbType = System.Data.SqlDbType.Image,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.PostImage
                    },
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok($"Imagen editada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se editó la imagen: " + ex.Message);
            }
        }
    }
}
