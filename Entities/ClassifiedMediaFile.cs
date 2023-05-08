using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAds.Entities
{
    [Table("classified_media_files")]
    public class ClassifiedMediaFile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("classified_id")]
        public int ClassifiedId { get; set; }

        public string Url { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
