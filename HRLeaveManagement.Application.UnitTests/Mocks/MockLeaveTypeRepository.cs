using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.Mocks;

/// <summary>
/// Provides a mock implementation of the <see cref="ILeaveTypeRepository"/> for unit testing purposes.
/// </summary>
public class MockLeaveTypeRepository
{
    /// <summary>
    /// Creates and returns a mock implementation of <see cref="ILeaveTypeRepository"/>.
    /// </summary>
    /// <returns>A mock repository with predefined behavior for testing.</returns>
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
        // Predefined list of LeaveType objects to simulate a data source.
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                DefaultDays = 20,
                Name = "TEST Annual Leave",
            },
            new LeaveType
            {
                Id = 2,
                DefaultDays = 10,
                Name = "TEST Sick Leave",
            },
            new LeaveType
            {
                Id = 3,
                DefaultDays = 5,
                Name = "TEST Unpaid Leave",
            },
        };

        // Create a mock repository for ILeaveTypeRepository.
        var mockRepo = new Mock<ILeaveTypeRepository>();

        // Setup the GetAsync method to return the predefined list of leave types.
        mockRepo.Setup(repo => repo.GetAsync()).ReturnsAsync(leaveTypes);

        // Setup the GetByIdAsync method to return the correct LeaveType based on the ID.
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => leaveTypes.FirstOrDefault(x => x.Id == id)!);

        // Setup the CreateAsync method to add a new LeaveType to the list.
        mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });


        // Setup the IsLeaveTypeUnique method to check if a leave type name is unique.
        mockRepo.Setup(r => r.IsLeaveTypeUnique(It.IsAny<string>()))
                .ReturnsAsync((string name) =>
                {
                    return !leaveTypes.Any(q => q.Name == name);
                });

        return mockRepo;
    }
}