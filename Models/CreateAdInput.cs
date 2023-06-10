using System.ComponentModel.DataAnnotations;

namespace MyAds.Models
{
    public class CreateAdvertisementInput
    {
        [Required(ErrorMessage = "Title of the advertisement is required.")]
        [MaxLength(64, ErrorMessage = "Nax length of title is 64 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category of the advertisement is required.")]
        public int CategoryId { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public List<IFormFile>? MediaFiles { get; set; }
    }
}
