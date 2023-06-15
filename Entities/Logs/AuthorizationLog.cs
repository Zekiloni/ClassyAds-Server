using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClassyAdsServer.Entities
{
    [Table("authorization_logs")]
    public class AuthorizationLog
    {
        [Required]
        [Column("ip_address")]
        public string IpAdress { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
