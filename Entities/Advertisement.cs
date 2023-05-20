﻿using MyAds.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAds.Entities
{
    [Table("advertisements")]
    public class Advertisement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Category")]
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
        [Column("is_negotiable")]
        public bool IsNegotiable { get; set; }

        [DefaultValue(null)]
        [Column("product_make")]
        public string Make { get; set; }

        [DefaultValue(null)]
        [Column("product_model")]
        public string Model { get; set; }

        [DefaultValue(AdvertisementStatus.Draft)]
        [Column("status")]
        public AdvertisementStatus Status { get; set; }

        [DefaultValue(false)]
        [Column("is_featured")]
        public bool IsFeatured { get; set; }

        [DefaultValue(false)]
        [Column("is_premium")]
        public bool IsPremium { get; set; }

        [Column("created_date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("expiring_at")]
        public DateTime ExpiringAt { get; set; } = DateTime.Now.AddDays(15);

        [DefaultValue(null)]
        [Column("updated_date")]
        public DateTime? UpdatedAt { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Category Category { get; set; }
    }
}