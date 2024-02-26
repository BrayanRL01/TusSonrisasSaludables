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
    public class UsersController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public UsersController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        #region GetUsers
        // GET: api/<UsersController>
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<VwUser>>> SP_GetUsersView()
        {
            if (_context.VwUsers == null)
            {
                return NotFound();
            }

            try
            {
                var users = await _context.VwUsers.FromSqlRaw("EXEC SP_GetUsersView").ToListAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UserInfo/{id}")]
        public async Task<ActionResult> SP_GetUserView(int id)
        {
            try
            {
                var users = await _context.VwUsers.FromSqlInterpolated($"EXEC SP_GetUserView {id}").ToListAsync();
                var user = users.FirstOrDefault();
                await _context.Database.CloseConnectionAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado el usuario: " + ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("User/{id}")]
        public async Task<ActionResult> SP_GetUser(int id)
        {
            try
            {
                var users = await _context.Users.FromSqlInterpolated($"EXEC SP_GetUser {id}").ToListAsync();
                var user = users.FirstOrDefault();
                await _context.Database.CloseConnectionAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado el usuario: " + ex.Message);
            }
        }

        [HttpGet("FindUser/{data}")]
        public async Task<ActionResult> SP_FindUser(string data)
        {
            try
            {
                var users = await _context.VwUsers.FromSqlInterpolated($"EXEC SP_FindUserView {data}").ToListAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado el usuario: " + ex.Message);
            }
        }
        #endregion

        #region PostUsers
        [HttpPost("AdminUser")]
        public async Task<IActionResult> PostAdminUser([FromBody] UserModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateAdminUser @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username," +
                    "@FirstName, @LastName, @BirthDate, @Email," +
                    "@Phone, @UserAddress, @Password";

                var param = new SqlParameter[]
                {
                    new()
                    {
                        ParameterName = "@TypeID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.TypeId
                    },
                     new()
                    {
                        ParameterName = "@GenreID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.GenreId
                    },
                     new()
                    {
                        ParameterName = "@ProvinceID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProvinceId
                    },
                     new()
                    {
                        ParameterName = "@IDNumber",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.IdNumber
                    },
                     new()
                    {
                        ParameterName = "@Username",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UserName
                    },
                     new()
                    {
                        ParameterName = "@FirstName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.FirstName
                    },
                     new()
                    {
                        ParameterName = "@LastName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.LastName
                    },
                     new()
                    {
                        ParameterName = "@BirthDate",
                        SqlDbType  = System.Data.SqlDbType.Date,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BirthDate
                    },
                     new()
                    {
                        ParameterName = "@Email",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Email
                    },
                     new()
                    {
                        ParameterName = "@Phone",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.PhoneNumber
                    },
                     new()
                    {
                        ParameterName = "@UserAddress",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UserAddress
                    },
                     new()
                    {
                        ParameterName = "@Password",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.PasswordHash
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Administrador creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear el usuario: " + ex.Message);
            }
        }
        #endregion

        #region PutUsers
        // PUT api/<UserController>/5
        [HttpPut("User")]
        public async Task<ActionResult<User>> PutUser([FromBody] UserModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditUser @ID, @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username," +
                    "@FirstName, @LastName, @BirthDate, @Email," +
                    "@Phone, @UserAddress";

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@ID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UserId
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
                        ParameterName = "@GenreID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.GenreId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@ProvinceID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProvinceId
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
                        ParameterName = "@Username",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UserName
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
                        ParameterName = "@UserAddress",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UserAddress
                    },
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar el usuario: " + ex.Message);
            }
        }
        #endregion

        #region DeleteUsers
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteUser {id}");
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Usuario eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar el usuario: " + ex.Message);
            }
        }
        #endregion

        #region Count
        [HttpGet("Count")]
        public async Task<ActionResult> GetCountUsers()
        {
            var count = new SqlParameter
            {
                ParameterName = "@Count",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 10,
                Direction = System.Data.ParameterDirection.Output
            };
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_CountUsers {count} OUTPUT");
            string result = (string)count.Value;
            await _context.Database.CloseConnectionAsync();

            return Ok(result);
        }
        #endregion
    }
}
