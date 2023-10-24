using BackEnd.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

                SqlParameter param1 = new()
                {
                    ParameterName = "@Email",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = login.Email
                };

                SqlParameter param2 = new()
                {
                    ParameterName = "@Password",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = login.PasswordHash
                };

                SqlParameter outputParam = new()
                {
                    ParameterName = "@Role",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 30,
                    Direction = System.Data.ParameterDirection.Output,
                };

                await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE SP_GetRoleUser {param1}, {param2}, {outputParam} OUTPUT");

                login.Roles = (string)outputParam.Value;

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Email),
                    new Claim(ClaimTypes.Role, login.Roles),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: config["JWT:ValidIssuer"],
                    audience: config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
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
        public async Task<ActionResult<User>> Register([FromBody] UserModel entity)
        {
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

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear el usuario: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser([FromBody] LoginModel model)
        {
            try
            {
                var user = await _context.Database.
                    ExecuteSqlInterpolatedAsync($"EXEC SP_GetEmailUser {model.Email}, {model.PasswordHash}");

                if (user.ToString() != null)
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
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("GetEmail")]
        public IActionResult GetEmail()
        {
            string email = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(email);
        }

        [HttpGet]
        [Route("UserEmail/{email}")]
        public async Task<IActionResult> GetUserEmail(string email)
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
                return Unauthorized(ex.Message);
            }
        }
    }
}

// Código del JWT #1
//var keyBytes = Encoding.ASCII.GetBytes(config["JWT:Secret"]);
//var claims = new ClaimsIdentity();

//claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Email));
//claims.AddClaim(new Claim(ClaimTypes.Role, roles.ToString()));


//var tokenDescriptor = new SecurityTokenDescriptor
//{
//    Subject = claims,
//    Expires = DateTime.UtcNow.AddHours(1),
//    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha512Signature)
//};

//var tokenHandler = new JwtSecurityTokenHandler();
//var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

//string tokenCreated = tokenHandler.WriteToken(tokenConfig);
//DateTime expiration = tokenConfig.ValidTo;

//return Ok("token: " + tokenCreated + ", expiration: " + expiration);


// Código del JWT #2
