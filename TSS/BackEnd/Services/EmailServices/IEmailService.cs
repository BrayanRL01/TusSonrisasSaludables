using BackEnd.Models;

namespace BackEnd.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmail(EmailModel model);

        Task SendEmail2(EmailModel model);
    }
}
