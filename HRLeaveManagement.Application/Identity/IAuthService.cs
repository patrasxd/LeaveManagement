using HRLeaveManagement.Application.Models.Identity;

namespace HRLeaveManagement.Application.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}