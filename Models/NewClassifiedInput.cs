using System.ComponentModel.DataAnnotations;

namespace MyAds.Models
{
    public class NewAdvertisementInput
    {
        [Required(ErrorMessage = "Title of the Advertisement is required.")]
        [MaxLength(64, ErrorMessage = "Nax length of title is 64 characters.")]
        public string Title { get; set; }

        public int CategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }


        public List<IFormFile> MediaFiles { get; set; }

    }
}
