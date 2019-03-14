using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftUni.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>(); 
    }
}