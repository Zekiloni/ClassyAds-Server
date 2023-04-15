using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Enums;

namespace WebApplication1.Models
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
        [Column("hashed_password")]
        public string HashedPassword { get; set; } = string.Empty;

        [Required]
        [DefaultValue(0)]
        [Column("administrator")]
        public UserAdminLevel Administrator { get; set; }

        public List<Classified>? Classifieds { get; set; }


        public bool IsAdmin {
            get { return this.Administrator == UserAdminLevel.Administrator; }
        }

        public bool IsSuperAddmin
        {
            get { return this.Administrator == UserAdminLevel.SuperAdministrator; }
        }
    }
}