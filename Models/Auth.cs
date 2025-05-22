using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Auth
    {
        [Key]
        public long UserId { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
    }
}
