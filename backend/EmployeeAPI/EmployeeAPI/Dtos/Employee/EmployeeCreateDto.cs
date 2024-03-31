using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Dtos.Employee
{
    public class EmployeeCreateDto
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
