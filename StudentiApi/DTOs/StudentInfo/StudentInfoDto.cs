using System.ComponentModel.DataAnnotations;

namespace StudentiApi.DTOs.StudentInfo
{
    public class StudentInfoDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
