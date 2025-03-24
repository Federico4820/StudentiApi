﻿using System.ComponentModel.DataAnnotations;

namespace StudentiApi.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [StringLength(50)]
        public required string Surname { get; set; }
        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        public DateTime Signed { get; set; } = DateTime.Now;
    }
}
