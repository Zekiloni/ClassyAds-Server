using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyAds.Entities
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("ParentCategory")]
        [Column("parent_category_id")]
        public int? ParentCategoryId { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Classified>? Classifieds { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual List<Category> ChildCategories { get; set; }
    }
}