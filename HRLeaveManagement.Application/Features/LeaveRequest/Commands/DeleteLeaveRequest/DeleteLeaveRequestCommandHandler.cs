﻿using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // get leave request and check if it exists
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        // delete leave request
        await _leaveRequestRepository.DeleteAsync(leaveRequest);

        // return Unit value
        return Unit.Value;
    }
}
