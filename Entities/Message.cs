using ClassyAdsServer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassyAdsServer.Entities
{
    public class Message
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Sender")]
        [Column("sender_user_id")]
        public int SenderId { get; set; }

        [ForeignKey("Target")]
        [Column("target_user_id")]
        public int TargetId { get; set; }

        [Required]
        [MinLength(2)]
        [Column("content")]
        public string Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("read_at")]
        public DateTime? ReadAt { get; set; }

        public User Sender { get; set; }

        public User Target { get; set; }
    }
}
