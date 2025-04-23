using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) => _leaveTypeRepository = leaveTypeRepository;

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // get leaveType and check if it exists
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(LeaveType), request.Id);

        // remove from database
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        // return void
        return Unit.Value;
    }
}
