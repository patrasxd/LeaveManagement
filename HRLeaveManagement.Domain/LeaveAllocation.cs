using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public int Period { get; set; }
}
