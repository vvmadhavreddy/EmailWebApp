using EmailWebAPI.Models;

namespace EmailWebAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
