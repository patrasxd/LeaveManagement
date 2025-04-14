using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveAlocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAlocationRepository(HRDatabaseContext context) : base(context)
    {
    }
}
