using EmailWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MailRequest> MailRequests { get; set; }
        public DbSet<MailAttachment> MailAttachments { get; set; }
    }
}
