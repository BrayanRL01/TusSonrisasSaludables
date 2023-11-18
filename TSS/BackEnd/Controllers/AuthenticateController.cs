using BackEnd.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;
        private readonly IConfiguration config;

        public AuthenticateController(TusSonrisasSaludablesContext context, IConfiguration _config)
        {
            _context = context;
            config = _config;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var user = await _context.Database.
                     ExecuteSqlInterpolatedAsync($"EXEC SP_AuthenticateUser {login.Email}, {login.PasswordHash}");

                login.Roles = await GetRole(login);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, login.Email),
                    new Claim(ClaimTypes.Role, login.Roles),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: config["JWT:ValidIssuer"],
                    audience: config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }
            catch (SqlException ex)
            {
                return BadRequest("No disponible: " + ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserModel entity)
        {
            if (ValidPassword(entity.PasswordHash) == false)
            {
                return BadRequest("La contraseña debe contener como mínimo 8 caracteres con un número, una letra mayúscula y un caracter especial.");
            }
            try
            {
                string Query = "EXEC SP_CreateUser @TypeID, @GenreID, @ProvinceID, @IDNumber, @Username," +
                    "@FirstName, @LastName, @BirthDate, @Email," +
                    "@Phone, @UserAddress, @Password";

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
                     new SqlParameter()
                    {
                        ParameterName = "@Password",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.PasswordHash
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok("Usuario creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear el usuario: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("GetRole")]
        public async Task<string> GetRole([FromBody] LoginModel model)
        {
            try
            {
                SqlParameter param1 = new()
                {
                    ParameterName = "@Email",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = model.Email
                };

                SqlParameter param2 = new()
                {
                    ParameterName = "@Password",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = model.PasswordHash
                };

                SqlParameter outputParam = new()
                {
                    ParameterName = "@Role",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 30,
                    Direction = System.Data.ParameterDirection.Output,
                };

                await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE SP_GetRoleUser {param1}, {param2}, {outputParam} OUTPUT");

                model.Roles = (string)outputParam.Value;

                return model.Roles;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [Route("GetUser")]
        public async Task<ActionResult> GetUser([FromBody] LoginModel model)
        {
            try
            {
                var users = await _context.Users.FromSqlInterpolated
                    ($"EXEC SP_GetEmailUser {model.Email}, {model.PasswordHash}").ToListAsync();
                var user = users.FirstOrDefault();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hubo un error: " + ex.Message);
            }
        }

        //[Authorize]
        [HttpGet("GetEmail/{email}")]
        public async Task<IActionResult> GetEmail(string email)
        {
            try
            {
                var users = await _context.Users.
                                   FromSqlInterpolated($"EXEC SP_EditUserEmail {email}").ToListAsync();
                var user = users.FirstOrDefault();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hubo un error: " + ex.Message);
            }
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            if (model.Email == null || model.Password == null || model.ConfirmPassword == null)
            {
                return BadRequest("Ha ocurrido un error al cambiar la contraseña, intente de nuevo.");
            }
            else if (model.Password.Length < 8 || ValidPassword(model.Password) == false || model.Password != model.ConfirmPassword)
            {
                return BadRequest("La contraseña debe contener un mínimo de 8 caractéres, un número, una letra mayúscula, un caracter especial y deben concordar " +
                    "ambas contraseñas, intente de nuevo.");
            }
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_ChangePassword {model.Email}, {model.Password}");
                await _context.SaveChangesAsync();
                return Ok("Se ha cambiado su contraseña correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ha ocurrido un error: {ex.Message}");
            }
        }

        public static bool ValidPassword(string password)
        {
            // Comprueba si la contraseña contiene al menos un número
            if (!Regex.IsMatch(password, @"\d") || !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[\W_]"))
            {
                return false;
            }
            return true;
        }
    }
}

