using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftUni.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<CourseTag> CourseTags { get; set; } = new HashSet<CourseTag>();
    }
}