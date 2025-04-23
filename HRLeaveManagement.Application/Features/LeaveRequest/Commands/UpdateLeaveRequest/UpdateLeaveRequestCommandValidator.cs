using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        // Includes validation rules from the BaseLeaveRequestValidator to ensure base leave request properties are validated.
        Include(new BaseLeaveRequestValidator(leaveTypeRepository));

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync((id, token) => LeaveRequestHelper.LeaveRequestMustExist(id, token, leaveRequestRepository))
            .WithMessage("{PropertyName} must be present");
    }
}
