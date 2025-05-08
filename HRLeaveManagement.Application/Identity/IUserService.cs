using HRLeaveManagement.Application.Models.Identity;

namespace HRLeaveManagement.Application.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
}
