using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailWebAPI.Models
{
    public class MailRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ToEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [NotMapped]
        public List<IFormFile> Attachments { get; set; }
    }
}
