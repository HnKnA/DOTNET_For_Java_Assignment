using System.ComponentModel.DataAnnotations;

namespace ClassManager.Api.Data.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(50)]
        public required string Subject { get; set; }

        [MaxLength(10)]
        public required string AccessCode { get; set; }
    }

}
