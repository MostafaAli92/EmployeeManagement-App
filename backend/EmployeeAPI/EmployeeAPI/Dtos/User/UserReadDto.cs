using System.Collections.Generic;

namespace EmployeeAPI.Dtos.User
{
    public class UserReadDto
    {
        public string userName {  get; set; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        public IList<string> roles { set; get; }
    }
}
