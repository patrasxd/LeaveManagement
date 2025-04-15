namespace HRLeaveManagement.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}
