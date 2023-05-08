using MyAds.Enums;

namespace MyAds.Models
{
    public class UserPublicViewOutput
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }

        public UserRole Role { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSuperAdmin { get; set; }
    }
}
