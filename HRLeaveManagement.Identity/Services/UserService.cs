using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Identity;
using HRLeaveManagement.Application.Models.Identity;
using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace HRLeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId) ?? 
            throw new NotFoundException($"User with {userId} was not found", userId);

        return new Employee
        {
            Email = employee.Email,
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(e => new Employee
        {
            Email = e.Email,
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName
        }).ToList();
    }
}
