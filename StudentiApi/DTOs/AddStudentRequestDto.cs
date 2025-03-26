using System.ComponentModel.DataAnnotations;

namespace StudentiApi.DTOs
{
    public class AddStudentRequestDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [StringLength(50)]
        public required string Surname { get; set; }
        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        public required string Description { get; set; }
    }
}
