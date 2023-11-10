using BackEnd.Models;
using Entities.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;
        private readonly IConfiguration _configuration;

        public EmailController(IConfiguration configuration, TusSonrisasSaludablesContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration["EmailInfo:Email"]));
                message.To.Add(MailboxAddress.Parse(model.To));
                message.Subject = model.Subject;
                message.Body = new TextPart("plain") { Text = model.Body };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_configuration["EmailInfo:Host"], int.Parse(_configuration["EmailInfo:Port"]), bool.Parse(_configuration["EmailInfo:EnableSsl"]));
                    await client.AuthenticateAsync(_configuration["EmailInfo:Email"], _configuration["EmailInfo:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return Ok("Correo enviado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] EmailModel model)
        {
            try
            {
                if (model.To != null)
                {
                    string password = RandomPassword();
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(_configuration["EmailInfo:Email"]));
                    message.To.Add(MailboxAddress.Parse(model.To));
                    message.Subject = "Sonrisas Saludables - Cambio de Contraseña";
                    message.Body = new TextPart("plain")
                    {
                        Text = "Tenga un cordial saludo, este correo es para efectuar el cambio de contraseña que solicitó, su nueva contraseña para el " +
                        "correo " + model.To + " es: " + password + "."
                    };

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync(_configuration["EmailInfo:Host"], int.Parse(_configuration["EmailInfo:Port"]), bool.Parse(_configuration["EmailInfo:EnableSsl"]));
                        await client.AuthenticateAsync(_configuration["EmailInfo:Email"], _configuration["EmailInfo:Password"]);
                        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_ResetPassword {model.To}, {password}");
                        await _context.SaveChangesAsync();
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }
                }
                return Ok("Se ha enviado correctamente un correo con su nueva contraseña.");
            }
            catch (Exception ex)
            {
                return BadRequest("No se efectuó el cambio de contraseña: " + ex.Message);
            }
        }

        public static string RandomPassword()
        {
            try
            {
                string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
                string obligatorios = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
                Random rand = new();

                return new string(Enumerable.Range(0, 8)
                    .Select(i => i < 3 ? obligatorios[rand.Next(obligatorios.Length)] : caracteres[rand.Next(caracteres.Length)])
                    .OrderBy(x => rand.Next())
                    .ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
