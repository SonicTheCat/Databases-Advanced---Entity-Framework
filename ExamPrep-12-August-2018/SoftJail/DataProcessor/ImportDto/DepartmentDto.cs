namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class DepartmentDto
    {
        [Required]
        [MinLength(3), MaxLength(25)]
        public string Name { get; set; }

        public CellDto[] Cells { get; set; }
    }
}