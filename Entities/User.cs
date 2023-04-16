using Microsoft.EntityFrameworkCore;
using MyAds.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MyAds.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("username")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [Column("email_address")]
        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [JsonIgnore]
        [Column("hashed_password")]
        public string HashedPassword { get; set; } = string.Empty;

        [Required]
        [DefaultValue(0)]
        [Column("administrator")]
        public UserAdminLevel Administrator { get; set; }

        [AllowNull]
        [DefaultValue(null)]
        [Column("last_login_at")]
        public DateTime? LastLoginAt { get; set; }

        public List<Classified>? Classifieds { get; set; }

        public bool IsAdmin
        {
            get { return Administrator == UserAdminLevel.Administrator; }
        }

        public bool IsSuperAdmin
        {
            get { return Administrator == UserAdminLevel.SuperAdministrator; }
        }
    }
}