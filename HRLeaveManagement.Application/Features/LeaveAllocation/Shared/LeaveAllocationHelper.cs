using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Shared;

public static class LeaveAllocationHelper
{
    public static async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token, ILeaveAllocationRepository leaveAllocationRepository)
    {
        return await leaveAllocationRepository.GetByIdAsync(id) != null;
    }
}
