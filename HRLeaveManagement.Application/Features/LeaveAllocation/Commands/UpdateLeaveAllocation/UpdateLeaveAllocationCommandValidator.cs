using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveAllocation.Shared;
using HRLeaveManagement.Application.Features.LeaveType.Shared;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync((id, token) => LeaveTypeHelper.LeaveTypeMustExist(id, token, leaveTypeRepository))
            .WithMessage("{PropertyName} must be present");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync((id, token) => LeaveAllocationHelper.LeaveAllocationMustExist(id, token, leaveAllocationRepository))
            .WithMessage("{PropertyName} must be present");
    }
}