using System.ComponentModel.DataAnnotations;

namespace ClassManager.Shared.DTOs
{
    public class RegisterDto
    {
        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(15, MinimumLength = 10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only numbers.")]
        public string Phone { get; set; } = string.Empty;

        [Required, MaxLength(250)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string? Role { get; set; } 
    }
}
