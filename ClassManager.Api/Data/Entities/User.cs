using System.ComponentModel.DataAnnotations;
using ClassManager.Shared;

namespace ClassManager.Api.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public required string Name { get; set; }
        [EmailAddress, MaxLength(150)]
        public required string Email { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10,15}$", ErrorMessage = "Phone number must only contains number and has 10-15 characters.")]
        public required string Phone { get; set; }
        [MaxLength(250)]
        public string? PasswordHash { get; set; }
        [MaxLength(15)]
        public string Role { get; set; } = nameof(UserRole.Department);
        public bool IsApproved { get; set; }
    }
}
