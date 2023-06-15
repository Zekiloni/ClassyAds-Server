using System.ComponentModel.DataAnnotations;

namespace ClassyAdsServer.Models
{
    public class CreateCategoryInput
    {
        [Required(ErrorMessage = "Category name is required.")]
        [MinLength(3, ErrorMessage = "Minimum length of category name is 3 characters.")]
        [MaxLength(32, ErrorMessage = "Maximum length of category name is 32 characters.")]
        public string Name { get; set; }

        [MaxLength(128, ErrorMessage = "Maximum length of category description is 64 characters.")]
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
