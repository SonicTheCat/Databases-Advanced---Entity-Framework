using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftUni.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Level { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        public decimal FullPrice { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<CourseTag> CourseTags { get; set; } = new HashSet<CourseTag>(); 
    }
}