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

        #region GetAppointments
        // GET: api/<AppointmentsController>
        [HttpGet("Appointments")]
        public async Task<ActionResult<IEnumerable<VwAppointment>>> SP_GetCitasView()
        {
            if (_context.VwAppointments == null)
            {
                return NotFound();
            }

            try
            {
                var users = await _context.VwAppointments.FromSqlRaw("EXEC SP_GetAppointmentsView").ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AdminAppointments")]
        public async Task<ActionResult<IEnumerable<VwAdminAppointment>>> SP_GetCitasAdminView()
        {
            if (_context.VwAdminAppointments == null)
            {
                return NotFound();
            }

            try
            {
                var citas = await _context.VwAdminAppointments.FromSqlRaw("EXEC SP_GetAdminAppointmentsView").ToListAsync();
                return Ok(citas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Counts
        [HttpGet("AvailableAppointments")]
        public async Task<ActionResult> AvailableAppointments()
        {
            try
            {
                SqlParameter outputParam = new()
                {
                    ParameterName = "@Count",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 5,
                    Direction = System.Data.ParameterDirection.Output,
                };

                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_AvailableAppointmentsCount {outputParam} OUTPUT");

                string count = (string)outputParam.Value;
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ReservedAppointments")]
        public async Task<ActionResult> ReservedAppointments()
        {
            try
            {
                SqlParameter outputParam = new()
                {
                    ParameterName = "@Count",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 5,
                    Direction = System.Data.ParameterDirection.Output,
                };

                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_ReservedAppointmentsCount {outputParam} OUTPUT");

                string count = (string)outputParam.Value;
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetBy
        // GET api/<AppointmentsController>/5
        [HttpGet("AppointmentInfo/{id}")]
        public async Task<ActionResult> SP_GetAppointment(int id)
        {
            try
            {
                var users = await _context.Appointments.FromSqlInterpolated($"EXEC SP_GetAppointment {id}").ToListAsync();
                var user = users.FirstOrDefault();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la cita: " + ex.Message);
            }
        }

        [HttpGet("Appointment/{id}")]
        public async Task<ActionResult> SP_GetAppointmentView(int id)
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

        [HttpGet("AdminAppointment/{id}")]
        public async Task<ActionResult> SP_GetAdminAppointment(int id)
        {
            try
            {
                var users = await _context.VwAdminAppointments.FromSqlInterpolated($"EXEC SP_GetAdminAppointmentView {id}").ToListAsync();
                var user = users.FirstOrDefault();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la cita: " + ex.Message);
            }
        }

        [HttpGet("UserAppointments/{email}")]
        public async Task<ActionResult> SP_UserAppointments(string email)
        {
            try
            {
                var users = await _context.VwAdminAppointments.FromSqlInterpolated($"EXEC SP_GetUserAppointments {email}").ToListAsync();
                if (users != null)
                {
                    return Ok(users);               
                }
                return StatusCode(500, "No hay citas reservadas.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la cita: " + ex.Message);
            }
        }
        #endregion

        #region PostAppointments
        // POST api/<AppointmentsController>
        [HttpPost("Appointment")]
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

                return Ok("Cita(as) creada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear la cita: " + ex.Message);
            }
        }
        #endregion

        #region PutAppoiments
        // PUT api/<AppointmentsController>/5
        [HttpPut("Appointment")]
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

                return Ok("Cita actualizada correctamente.");
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
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteAppointment {id}");
                await _context.SaveChangesAsync();
                return Ok("Cita eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar la cita: " + ex.Message);
            }
        }
        #endregion

        #region Confirm and Cancel
        [HttpPut("ConfirmAppointment")]
        public async Task<ActionResult> ConfirmAppointment([FromBody] AppointmentModel model)
        {
            try
            {
                if (model.Email == null)
                {
                    return BadRequest();
                }
                else
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_ConfirmAppointment {model.AppointmentId}, {model.Email}");
                    await _context.SaveChangesAsync();
                    return Ok("Cita reservada correctamente.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se ha podido confirmar la cita: {ex.Message}");
            }
        }

        [HttpPut("CancelAppointment")]
        public async Task<ActionResult> CancelAppointment([FromBody] AppointmentModel model)
        {
            try
            {
                if (model.Email == null)
                {
                    return BadRequest();
                }
                else
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_CancelAppointment {model.AppointmentId}, {model.Email}");
                    await _context.SaveChangesAsync();
                    return Ok("Cita cancelada correctamente.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se ha podido confirmar la cita: {ex.Message}");
            }
        }
        #endregion

        #region NextAppointment
        [HttpGet("NextAppointment")]
        public async Task<ActionResult> NextAppointment()
        {
            try
            {
                DateTime date = DateTime.Now;
                SqlParameter outputParam = new()
                {
                    ParameterName = "@Next",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 30,
                    Direction = System.Data.ParameterDirection.Output,
                };

                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_NextAppointment {outputParam} OUTPUT");

                string next = (string)outputParam.Value;
                return Ok(next);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
