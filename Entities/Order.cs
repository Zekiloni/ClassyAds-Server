using Microsoft.EntityFrameworkCore;
using MyAds.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAds.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }


        [Required]
        [DefaultValue(OrderStatus.Draft)]
        [Column("status")]
        public OrderStatus Status { get; set; }

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Required]
        [ForeignKey("user_id")]
        public int UserId { get; set; }

        public User? User { get; set; }

        public Category? Category { get; set; }
    }
}