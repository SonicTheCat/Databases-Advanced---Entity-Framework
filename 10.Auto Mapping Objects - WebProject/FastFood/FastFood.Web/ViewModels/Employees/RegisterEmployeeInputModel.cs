using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Employees
{
    public class RegisterEmployeeInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        public string PositionName { get; set; }

        [Required]
        public string Address { get; set; }
    }
}