using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Shared;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed {MaxLength} characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed {ComparisonValue}")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than {ComparisonValue}");

        RuleFor(q => q)
            .MustAsync((command, token) => LeaveTypeHelper.LeaveTypeNameUnique(command.Name, token, leaveTypeRepository))
            .WithMessage("Leave Type already exists");
    }
}
