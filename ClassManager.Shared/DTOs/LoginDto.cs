using System.ComponentModel.DataAnnotations;

namespace ClassManager.Shared.DTOs
{
    public class LoginDto
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]  
        public string Password { get; set; }
    }
}
