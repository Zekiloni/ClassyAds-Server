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
        [Column("category_description")]
        [MaxLength(64)]
        public string Description { get; set; }

        [DefaultValue(null)]
        [ForeignKey("parent_category_id")]
        public int? ParentCategoryId { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Classified>? Classifieds { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> ChildCategories { get; set; }
    }
}