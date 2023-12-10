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
                var users = await _context.VwRecomendations.FromSqlRaw("EXEC SP_GetRecomendations").ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Recomendation")]
        public async Task<ActionResult<Recomendation>> PostRecomendations([FromBody] RecomendationModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateRecomendation @User, @Specialty, @Info";

                var param = new SqlParameter[]
               {
                    
                    new SqlParameter()
                    {
                        ParameterName = "@User",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UserID
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Specialty",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyID
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Info",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Information
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
    }
}
