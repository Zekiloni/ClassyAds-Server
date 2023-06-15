using System.ComponentModel.DataAnnotations;

namespace ClassyAdsServer.Models
{
    public class RegisterUserInput
    {

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [MaxLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [MaxLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string EmailAddress { get; set; }
    }
}
