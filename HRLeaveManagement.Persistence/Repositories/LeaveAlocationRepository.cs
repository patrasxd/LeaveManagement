using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveAlocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAlocationRepository(HRDatabaseContext context) : base(context)
    {
    }

    public async Task AddAllocation(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(e => e.EmployeeId == userId && e.LeaveTypeId == leaveTypeId && e.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await _context.LeaveAllocations
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        return await _context.LeaveAllocations
            .Where(e => e.EmployeeId == userId)
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        throw new NotImplementedException();
    }
}
