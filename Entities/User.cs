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
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column("email_address")]
        [MaxLength(50)]
        public string? EmailAddress { get; set; }

        [MaxLength(20)]
        [DefaultValue(null)]
        [Column("phone")]
        public string? Phone { get; set; }

        [MaxLength(100)]
        [DefaultValue(null)]
        [Column("street")]
        public string? Street { get; set; }

        [MaxLength(50)]
        [DefaultValue(null)]
        [Column("city")]
        public string? City { get; set; }

        [MaxLength(50)]
        [DefaultValue(null)]
        [Column("province")]
        public string? Province { get; set; }

        [MaxLength(10)]
        [DefaultValue(null)]
        [Column("postal_code")]
        public string? PostalCode { get; set; }

        [Required]
        [JsonIgnore]
        [Column("hashed_password")]
        public string HashedPassword { get; set; } = string.Empty;

        [Required]
        [DefaultValue(UserRole.Customer)]
        [Column("role")]
        public UserRole Role { get; set; }

        [AllowNull]
        [DefaultValue(null)]
        [Column("last_login_at")]
        public DateTime? LastLoginAt { get; set; }

        [AllowNull]
        [DefaultValue(null)]
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Classified>? Orders { get; set; }

        public bool IsAdmin
        {
            get { return Role == UserRole.Admin; }
        }

        public bool IsSuperAdmin
        {
            get { return Role == UserRole.SuperAdmin; }
        }
    }
}