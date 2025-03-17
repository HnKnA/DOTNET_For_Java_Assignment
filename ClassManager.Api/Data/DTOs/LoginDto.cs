using System.ComponentModel.DataAnnotations;

namespace ClassManager.Api.Data.DTOs
{
    public class LoginDto
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]  
        public string Password { get; set; }
    }
}
