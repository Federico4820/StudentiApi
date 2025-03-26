using System.ComponentModel.DataAnnotations;
using StudentiApi.DTOs.StudentInfo;

namespace StudentiApi.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public required string Name { get; set; }
        [StringLength(50)]
        public required string Surname { get; set; }
        [StringLength(50)]
        public required string Email { get; set; }        
        public DateTime Signed { get; set; }
        public StudentInfoDto StudentInfo { get; set; }
    }
}
