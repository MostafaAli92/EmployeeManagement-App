using EmployeeAPI.Dtos.User;
using EmployeeManagement.Data.Models;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string, UserReadDto)> Login(LoginModel model);
    }
}
