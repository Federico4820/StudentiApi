using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentiApi.Models
{
    public class StudentInfo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Description { get; set; }
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public Guid StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
    }
}
