using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ClassyAdsServer.Entities
{
    [Table("advertisement_media_files")]
    public class AdvertisementMediaFile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("advertisement_id")]
        public int AdvertisementId { get; set; }

        [Required]
        [Column("url")]
        public string Url { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [AllowNull]
        [DefaultValue(null)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;
    }
}
