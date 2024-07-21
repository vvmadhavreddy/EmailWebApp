using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailWebAPI.Models
{
    public class MailAttachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        [Required]
        public string ContentType { get; set; }

        [ForeignKey("MailRequestId")]
        public MailRequest MailRequest { get; set; }
        public int MailRequestId { get; set; }
    }
}
