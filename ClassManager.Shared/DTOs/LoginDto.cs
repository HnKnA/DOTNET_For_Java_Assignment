using System.ComponentModel.DataAnnotations;

namespace ClassManager.Shared.DTOs
{
    public class LoginDto
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public required string UserName { get; set; } = string.Empty;
        [Required]  
        public required string Password { get; set; } = string.Empty;
    }
}
