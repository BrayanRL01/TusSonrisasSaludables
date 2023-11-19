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
    public class RecordsController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public RecordsController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        [HttpGet("Records")]
        public async Task<ActionResult<IEnumerable<VwRecord>>> SP_GetRecordView()
        {
            if (_context.VwRecords == null)
            {
                return NotFound();
            }

            try
            {
                var records = await _context.VwRecords.FromSqlRaw("EXEC SP_GetRecordsView").ToListAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UserRecords/{email}")]
        public async Task<ActionResult> SP_GetUserRecords(string email)
        {
            if (_context.VwRecords == null)
            {
                return NotFound();
            }

            try
            {
                var records = await _context.VwRecords.FromSqlInterpolated($"EXEC SP_GetPatientRecord {email}").ToListAsync();
                if (records == null)
                {
                    return StatusCode(500, "No hay registros disponibles.");
                }
                return Ok(records);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region GetID
        [HttpGet("RecordInfo/{id}")]
        public async Task<ActionResult> SP_GetRecordView(int id)
        {
            try
            {
                var records = await _context.VwRecords.FromSqlInterpolated($"EXEC SP_GetRecordView {id}").ToListAsync();
                var record = records.FirstOrDefault();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado: " + ex.Message);
            }
        }

        [HttpGet("Record/{id}")]
        public async Task<ActionResult> SP_GetRecord(int id)
        {
            try
            {
                var records = await _context.PatientRecords.FromSqlInterpolated($"EXEC SP_GetRecord {id}").ToListAsync();
                var record = records.FirstOrDefault();

                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado: " + ex.Message);
            }
        }
        #endregion

        #region Post
        // POST api/<RecordController>
        [HttpPost("Record")]
        public async Task<ActionResult<PatientRecord>> PostRecord([FromBody] RecordModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateRecord @UserID, @DoctorID, @ProcedureID, @Diagnoses, @Symptoms, @Treatment";

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@UserID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UserId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@DoctorID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@ProcedureID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProcedureId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Diagnoses",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Diagnoses
                    },
                      new SqlParameter()
                    {
                        ParameterName = "@Symptoms",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Symptoms
                    },
                       new SqlParameter()
                    {
                        ParameterName = "@Treatment",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Treatment
                    },

                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok("Registro creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear: " + ex.Message);
            }
        }
        #endregion

        #region PutRecords
        // PUT api/<RecordController>/5
        [HttpPut("Record")]
        public async Task<ActionResult<PatientRecord>> PutRecord([FromBody] RecordModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditRecord @ID, @Diagnoses, @Symptoms, @Treatment";

                var param = new SqlParameter[]
               {
                    new SqlParameter()
                    {

                        ParameterName = "@ID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.RecordId
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Diagnoses",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Diagnoses
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Symptoms",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Symptoms
                    },
                      new SqlParameter()
                    {
                        ParameterName = "@Treatment",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Treatment
                    }
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok("Registro actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar: " + ex.Message);
            }
        }
        #endregion

        #region DeleteRecords
        // DELETE api/<RecordController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecord(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteRecord {id}");
                await _context.SaveChangesAsync();
                return Ok("Registro eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar: " + ex.Message);
            }
        }
        #endregion
    }
}
