using EmailWebAPI.Models;
using EmailWebAPI.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using EmailWebAPI.Repositories;

namespace EmailWebAPI.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IMailRequestRepository _mailRequestRepository;
        public MailService(IOptions<MailSettings> mailSettings, IMailRequestRepository mailRequestRepository)
        {
            _mailSettings = mailSettings.Value;
            _mailRequestRepository = mailRequestRepository;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            // Creating a mime message and attaching the required parameters
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();
            var mailAttachments = new List<MailAttachment>();

            if (mailRequest.Attachments != null)
            {
                // Attaching all the attachments to the email
                foreach (var attachment in mailRequest.Attachments)
                {
                    if (attachment.Length > 0)
                    {
                        byte[] fileBytes;
                        using (var ms = new MemoryStream())
                        {
                            await attachment.CopyToAsync(ms);
                            fileBytes = ms.ToArray();

                            var mailAttachment = new MailAttachment
                            {
                                FileName = attachment.FileName,
                                FileContent = fileBytes,
                                ContentType = attachment.ContentType
                            };
                            mailAttachments.Add(mailAttachment);
                        }
                        builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            // SMTP server connection and authentication
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            // Save to database
            await _mailRequestRepository.AddMailRequestAsync(mailRequest, mailAttachments);

            // Send email
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
