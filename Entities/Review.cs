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

        [ForeignKey("Advertisement")]
        public int AdvertisementId { get; set; }

        [ForeignKey("User")]
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
