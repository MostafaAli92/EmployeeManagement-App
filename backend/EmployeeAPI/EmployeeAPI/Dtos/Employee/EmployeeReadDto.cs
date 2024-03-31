using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Dtos.Employee
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }
    }
}
