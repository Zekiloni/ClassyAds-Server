using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassyAdsServer.Entities
{
    [Table("notifications")]
    public class Notification
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(64)]
        [Column("title")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Column("description")]
        public string Content { get; set; }

        [DefaultValue(false)]
        [Column("is_read")]
        public bool IsRead { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User User { get; set; }
    }
}
