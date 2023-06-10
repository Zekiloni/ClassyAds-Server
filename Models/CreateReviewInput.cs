using System.ComponentModel.DataAnnotations;

namespace ClassyAdsServer.Models
{
    public class CreateReviewInput
    {
        [Required(ErrorMessage = "Rating is required.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Review comment is requried.")]
        [MinLength(3, ErrorMessage = "Minimum lenght of review comment is 3 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Advertisement id is requried.")]
        public int AdvertisementId { get; set; }
    }
}
