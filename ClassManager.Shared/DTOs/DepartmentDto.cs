using System.ComponentModel.DataAnnotations;

namespace ClassManager.Shared.DTOs
{
    public class DepartmentDto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Subject { get; set; } = string.Empty;

        [MaxLength(10)]
        public string AccessCode { get; set; } = string.Empty;

    }
}
