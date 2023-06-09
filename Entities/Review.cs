using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyAds.Entities
{
    [Table("reviews")]
    public class Review
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Advertisement")]
        [Column("advertisement_id")]
        public int AdvertisementId { get; set; }

        [Required]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("comment")]
        public string Comment { get; set; }

        [Required]
        [Column("rating")]
        public int Rating { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Advertisement Advertisement { get; set; }

        public virtual User User { get; set; }
    }
}
