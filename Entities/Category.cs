using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyAds.Entities
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int Id { get; set; }

        [Required]
        [Column("category_name")]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        [MaxLength(64)]
        [Column("category_description")]
        public string Description { get; set; }

        [DefaultValue(null)]
        [ForeignKey("ParentCategory")]
        [Column("parent_category_id")]
        public int? ParentCategoryId { get; set; }

        [DefaultValue(false)]
        [Column("is_featured")]
        public bool IsFeatured { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DefaultValue(null)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual List<Advertisement> Advertisements { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual List<Category> ChildCategories { get; set; }
    }
}