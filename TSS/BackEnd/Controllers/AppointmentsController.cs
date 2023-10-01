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
    public class AppointmentsController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public AppointmentsController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<AppointmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VwAppointment>>> SP_GetCitasView()
        {
            if (_context.VwAppointments == null)
            {
                return NotFound();
            }

            try
            {
                var users = await _context.VwAppointments.FromSqlRaw("EXEC SP_GetAppointmentsView").ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GETAdminCitas")]
        public async Task<ActionResult<IEnumerable<VwAdminAppointment>>> SP_GetCitasAdminView()
        {
            if (_context.VwAdminAppointments == null)
            {
                return NotFound();
            }

            try
            {
                var citas = await _context.VwAdminAppointments.FromSqlRaw("EXEC SP_GetAdminAppointmentsView").ToListAsync();
                return citas;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AppointmentsController>/5

        [HttpGet("GetAppointment/{id}")]
        public async Task<ActionResult> SP_GetAppointment(int id)
        {
            try
            {
                var users = await _context.VwAppointments.FromSqlInterpolated($"EXEC SP_GetAppointmentView {id}").ToListAsync();
                var user = users.FirstOrDefault();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la cita: " + ex.Message);
            }
        }

        [HttpGet("FindAppointment/{data}")]
        public async Task<ActionResult> SP_FindAppointment(int data)
        {
            try
            {
                var users = await _context.VwAppointments.FromSqlInterpolated($"EXEC SP_FindAppointmentView {data}").ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la cita: " + ex.Message);
            }
        }


        // POST api/<AppointmentsController>
        [HttpPost("PostAppointment")]
        public async Task<ActionResult<Appointment>> PostAppointment([FromBody] AppointmentModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateAppointment @DoctorID, @SpecialtyID, @StartTime, @EndTime";

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@DoctorID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@SpecialtyID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@StartTime",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.StartTime
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@EndTime",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.EndTime
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear la cita: " + ex.Message);
            }
        }

        #region PutAppoiments
        // PUT api/<AppointmentsController>/5
        [HttpPut("PutAppointment")]

        public async Task<ActionResult<Appointment>> PutAppointment([FromBody] AppointmentModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditAppointment @AppointmentID, @DoctorID, @SpecialtyID, @StartTime, @EndTime";

                var param = new SqlParameter[]
               {
                    new SqlParameter()
                    {

                        ParameterName = "@AppointmentID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.AppointmentId
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
                        ParameterName = "@SpecialtyID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.SpecialtyId
                    },
                      new SqlParameter()
                    {
                        ParameterName = "@StartTime",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.StartTime
                    },
                       new SqlParameter()
                    {
                        ParameterName = "@EndTime",
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.EndTime
                       }
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar la cita: " + ex.Message);
            }
        }

        #endregion

        #region DeleteAppointments
        // DELETE api/<AppointmentsController>/5
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteAppointment(int id)
        {
            {
                try
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteAppointment {id}");
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "No se pudo eliminar la cita: " + ex.Message);
                }
            }
            #endregion
        }
    }
}
