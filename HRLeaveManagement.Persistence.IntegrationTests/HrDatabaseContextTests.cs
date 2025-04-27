using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HRLeaveManagement.Persistence.IntegrationTests;

/// <summary>
/// Provides integration tests for the <see cref="HrDatabaseContext"/> to verify
/// that entity lifecycle events, such as setting the DateCreated and DateModified
/// properties, are handled correctly.
/// </summary>
public class HrDatabaseContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTests()
    {
        // Initialize the in-memory database for testing
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        // Arrange: Create a new LeaveType entity
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 20,
            Name = "TEST Annual Leave",
        };

        // Act: Add the entity to the database and save changes
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert: Verify that the DateCreated property is set
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        // Arrange: Create a new LeaveType entity
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 20,
            Name = "TEST Annual Leave",
        };

        // Act: Add the entity to the database and save changes
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert: Verify that the DateModified property is set
        leaveType.DateModified.ShouldNotBeNull();
    }
}