using WebApi.Models;

namespace WebApi.Services.EmailService
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}
