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
    public class DoctorController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public DoctorController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        // GET: api/<DoctorsController>
        [HttpGet("Doctors")]
        public async Task<ActionResult<IEnumerable<VwDoctor>>> SP_GetDoctorsView()
        {
            if (_context.VwDoctors == null)
            {
                return NotFound();
            }

            try
            {
                var doctors = await _context.VwDoctors.FromSqlRaw("EXEC SP_GetDoctorsView").ToListAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<DoctorsController>/5
        [HttpGet("DoctorInfo/{id}")]
        public async Task<ActionResult> SP_GetDoctorView(int id)
        {
            try
            {
                var doctors = await _context.VwDoctors.FromSqlInterpolated($"EXEC SP_GetDoctorView {id}").ToListAsync();
                var doctor = doctors.FirstOrDefault();

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "El doctor no existe " + ex.Message);
            }
        }

        [HttpGet("Doctor/{id}")]
        public async Task<ActionResult> SP_GetDoctor(int id)
        {
            try
            {
                var doctors = await _context.Doctors.FromSqlInterpolated($"EXEC SP_GetDoctor {id}").ToListAsync();
                var doctor = doctors.FirstOrDefault();

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "El doctor no existe: " + ex.Message);
            }
        }

        [HttpGet("FindDoctors/{data}")]
        public async Task<ActionResult> SP_FindDoctors(int data)
        {
            try
            {
                var users = await _context.VwDoctors.FromSqlInterpolated($"EXEC SP_FindDoctorsView {data}").ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "El doctor no existe " + ex.Message);
            }
        }


        // POST api/<DoctorsController>
        [HttpPost("Doctor")]
        public async Task<ActionResult<Doctor>> PostDoctors([FromBody] DoctorModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateDoctor @TypeID, @SpecialtyID, @GenreID, @IDNumber, @DoctorName," +
                    "@FirstName, @LastName, @BirthDate, @Email, @Phone, @Photo";

                var param = new SqlParameter[]
               {
                    new SqlParameter()
                    {
                        ParameterName = "@TypeID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.TypeId
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
                        ParameterName = "@GenreID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.GenreId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@IDNumber",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.IdNumber
                    },
                      new SqlParameter()
                    {
                        ParameterName = "@DoctorName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorName
                    },

                     new SqlParameter()
                    {
                        ParameterName = "@FirstName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.FirstName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@LastName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.LastName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@BirthDate",
                        SqlDbType  = System.Data.SqlDbType.Date,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BirthDate
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Email",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Email
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Phone",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.PhoneNumber
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Photo",
                        SqlDbType = System.Data.SqlDbType.Image,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorPhoto
                    }
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok($"Doctor/a {entity.DoctorName} {entity.FirstName} {entity.LastName} creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se creó el doctor: " + ex.Message);
            }
        }
        //aqui quede

        // PUT api/<DoctorsController>/5
        [HttpPut("Doctor")]
        public async Task<ActionResult> PutDoctors([FromBody] DoctorModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditDoctor @DoctorID, @TypeID, @SpecialtyID, @GenreID, @IDNumber, @DoctorName," +
                       "@FirstName, @LastName, @BirthDate, @Email, @Phone";

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
                        ParameterName = "@TypeID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.TypeId
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
                        ParameterName = "@GenreID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.GenreId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@IDNumber",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.IdNumber
                    },
                      new SqlParameter()
                    {
                        ParameterName = "@DoctorName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorName
                    },

                     new SqlParameter()
                    {
                        ParameterName = "@FirstName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.FirstName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@LastName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.LastName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@BirthDate",
                        SqlDbType  = System.Data.SqlDbType.Date,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BirthDate
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Email",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Email
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Phone",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.PhoneNumber
                    }
               };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok($"Doctor/a {entity.DoctorName} {entity.FirstName} {entity.LastName} actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se creó el doctor: " + ex.Message);
            }
        }

        // DELETE api/<UserController>/5
        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctors(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteDoctor {id}");
                await _context.SaveChangesAsync();
                return Ok("Doctor/a eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar: " + ex.Message);
            }
        }
        #endregion

        [HttpPut("ChangeImage")]
        public async Task<ActionResult<Recomendation>> ChangeImage([FromBody] DoctorModel entity)
        {
            try
            {
                string Query = "EXEC SP_ChangeDoctorImage @ID, @Image";

                var param = new SqlParameter[]
               {

                    new SqlParameter()
                    {
                        ParameterName = "@ID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Image",
                        SqlDbType = System.Data.SqlDbType.Image,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.DoctorPhoto
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
