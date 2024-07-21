using EmailWebAPI.Data;
using EmailWebAPI.Models;

namespace EmailWebAPI.Repositories
{
    public class MailRequestRepository : IMailRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public MailRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMailRequestAsync(MailRequest mailRequest, List<MailAttachment> attachments)
        {
            await _context.MailRequests.AddAsync(mailRequest);
            await _context.SaveChangesAsync();

            foreach (var attachment in attachments)
            {
                attachment.MailRequestId = mailRequest.Id;
                await _context.MailAttachments.AddAsync(attachment);
            }

            await _context.SaveChangesAsync();
        }
    }
}
