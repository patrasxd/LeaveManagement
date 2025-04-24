using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.MappingProfiles;
using HRLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HRLeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands;

/// <summary>
/// Unit tests for the CreateLeaveTypeCommandHandler.
/// </summary>
public class CreateleaveTypeCommandTests
{
    private readonly IMapper _mapper;
    private Mock<ILeaveTypeRepository> _mockRepo;

    /// <summary>
    /// Initializes the test class by setting up the mock repository and AutoMapper configuration.
    /// </summary>
    public CreateleaveTypeCommandTests()
    {
        // Set up a mock repository for ILeaveTypeRepository.
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        // Configure AutoMapper with the LeaveTypeProfile mapping profile.
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    /// <summary>
    /// Tests the CreateLeaveTypeCommandHandler with valid data.
    /// Ensures that a new leave type is successfully created and added to the repository.
    /// </summary>
    [Fact]
    public async Task CreateleaveTypeTest_ValidData()
    {
        // Arrange: Create an instance of the command handler.
        var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object);

        // Act: Handle the command with valid data.
        await handler.Handle(new CreateLeaveTypeCommand()
        {
            Name = "Test data",
            DefaultDays = 2
        }, CancellationToken.None);

        // Assert: Verify that the leave type count in the repository has increased.
        var leaveTypes = await _mockRepo.Object.GetAsync();
        leaveTypes.Count.ShouldBe(4); // Assuming the mock repository initially contains 3 leave types.
    }

    /// <summary>
    /// Tests the CreateLeaveTypeCommandHandler with invalid data.
    /// Ensures that a BadRequestException is thrown when invalid data is provided.
    /// </summary>
    [Fact]
    public async Task CreateLeaveTypeTest_InvalidData_ThrowsBadRequestException()
    {
        // Arrange: Create an instance of the command handler.
        var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object);

        // Act & Assert: Verify that a BadRequestException is thrown for invalid data.
        await Should.ThrowAsync<BadRequestException>(async () =>
        {
            await handler.Handle(new CreateLeaveTypeCommand()
            {
                Name = "", // Invalid name (e.g., empty or null)
                DefaultDays = -1 // Invalid DefaultDays (e.g., negative value)
            }, CancellationToken.None);
        });
    }
}