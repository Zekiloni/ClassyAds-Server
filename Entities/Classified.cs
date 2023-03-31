using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Enums;

namespace WebApplication1.Models
{
    [Table("classifieds")]
    public class Classified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("description")]
        public string Description { get; set; } 

        [Required]
        [Column("price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Column("status")]
        public ClassifiedStatus Status { get; set; }

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User? User { get; set; }
    }
}