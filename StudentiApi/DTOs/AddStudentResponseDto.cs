using System.ComponentModel.DataAnnotations;

namespace StudentiApi.DTOs
{
    public class AddStudentResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
