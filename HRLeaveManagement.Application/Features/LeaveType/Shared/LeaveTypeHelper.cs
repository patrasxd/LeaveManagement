using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

namespace HRLeaveManagement.Application.Features.LeaveType.Shared;

public static class LeaveTypeHelper
{
    public static async Task<bool> LeaveTypeMustExist(int id, CancellationToken token, ILeaveTypeRepository leaveTypeRepository)
    {
        return await leaveTypeRepository.GetByIdAsync(id) != null;
    }
    public static async Task<bool> LeaveTypeNameUnique(string name, CancellationToken token, ILeaveTypeRepository leaveTypeRepository)
    {
        return await leaveTypeRepository.IsLeaveTypeUnique(name);
    }
}
