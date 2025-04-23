using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Shared;

public static class LeaveRequestHelper
{
    public static async Task<bool> LeaveRequestMustExist(int id, CancellationToken token, ILeaveRequestRepository leaveRequestRepository)
    {
        return await leaveRequestRepository.GetByIdAsync(id) != null;
    }
}
