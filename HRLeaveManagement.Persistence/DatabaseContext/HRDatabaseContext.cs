using HRLeaveManagement.Domain;
using HRLeaveManagement.Domain.Common;
using HRLeaveManagement.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.DatabaseContext;

public class HRDatabaseContext : DbContext
{
    public HRDatabaseContext(DbContextOptions<HRDatabaseContext> options) : base(options)
    {

    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDatabaseContext).Assembly);
        //modelBuilder.ApplyConfiguration(new LeaveTypeConfiguration()); <- this is not needed anymore as we are using ApplyConfigurationsFromAssembly

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
