using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Dtos.Employee
{
    public class EmployeeUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Department { get; set; }
    }
}
