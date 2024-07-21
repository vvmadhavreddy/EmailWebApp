using EmailWebAPI.Models;

namespace EmailWebAPI.Repositories
{
    public interface IMailRequestRepository
    {
        Task AddMailRequestAsync(MailRequest mailRequest, List<MailAttachment> attachments);
    }
}
