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
    public class ProcedureController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public ProcedureController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<ProcedureController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VwProcedure>>> SP_GetProcedureView ()
        {
            if (_context.VwProcedures == null)
            {
                return NotFound();
            }

            try
            {
                var procedures = await _context.VwProcedures.FromSqlRaw("EXEC SP_GetProceduresView ").ToListAsync();
                return procedures;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProcedureController>/5
        [HttpGet("GetRecord/{id}")]
        public async Task<ActionResult> SP_GetProcedureView (int id)
        {
            try
            {
                var procedures = await _context.VwProcedures.FromSqlInterpolated($"EXEC SP_GetProcedureView  {id}").ToListAsync();
                var procedure = procedures.FirstOrDefault();

                return Ok(procedure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "El procedimiento no existe " + ex.Message);
            }
        }

        // POST api/<ProcedureController>
        [HttpPost("PostProcedure")]
        public async Task<ActionResult<ClinicProcedure>> PostProcedure([FromBody] ProcedureModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateProcedure @Procedure";

                var param = new SqlParameter[]
                {
                   new SqlParameter()
                    {
                        ParameterName = "@Procedure",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProcedureName
                    }
                     
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear: " + ex.Message);
            }
        }

        #region PutProcedures
        // PUT api/<ProcedureController>/5
        [HttpPut]
        public async Task<ActionResult<ClinicProcedure>> PutProcedure([FromBody] ProcedureModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditProcedure @ID, @Procedure";

                var param = new SqlParameter[]
               {
                    new SqlParameter()
                    {

                        ParameterName = "@ID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProcedureId
                    },
                     new SqlParameter()
                     {
                         ParameterName = "@Procedure",
                         SqlDbType = System.Data.SqlDbType.VarChar,
                         Direction = System.Data.ParameterDirection.Input,
                         Value = entity.ProcedureName
                     }
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar: " + ex.Message);
            }
        }
        #endregion

        #region DeleteProcedure
        // DELETE api/<ProcedureController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProcedure(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteProcedure {id}");
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar: " + ex.Message);
            }
        }
        #endregion
    }
}

