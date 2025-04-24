using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Shared;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync((id, token) => LeaveTypeHelper.LeaveTypeMustExist(id, token, leaveTypeRepository))
            .WithMessage("{PropertyName} must be present.");
    }
}