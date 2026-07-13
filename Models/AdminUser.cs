using System.ComponentModel.DataAnnotations;

namespace LakshBeautyStudio.Models
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // BCrypt hashed, never plain text
    }
}
