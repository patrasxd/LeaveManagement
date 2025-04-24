using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.MappingProfiles;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagement.Domain;
using Moq;
using Shouldly;
using MediatR;

namespace HRLeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands;

/// <summary>
/// Unit tests for the UpdateLeaveTypeCommandHandler.
/// </summary>
public class UpdateLeaveTypeCommandHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> _logger;

    /// <summary>
    /// Initializes the test class by setting up the mock repository, AutoMapper, and logger.
    /// </summary>
    public UpdateLeaveTypeCommandHandlerTests()
    {
        // Set up a mock repository for ILeaveTypeRepository.
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        // Configure AutoMapper with the LeaveTypeProfile mapping profile.
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        // Initialize a mock logger for the command handler.
        _logger = new Mock<IAppLogger<UpdateLeaveTypeCommandHandler>>();
    }

    /// <summary>
    /// Tests the UpdateLeaveTypeCommandHandler with valid data.
    /// Ensures that the leave type is successfully updated in the repository.
    /// </summary>
    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateLeaveType()
    {
        // Arrange: Create an instance of the command handler and a valid update command.
        var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _logger.Object);
        var command = new UpdateLeaveTypeCommand
        {
            Id = 1,
            Name = "Updated Annual Leave",
            DefaultDays = 15
        };

        // Act: Execute the command handler.
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert: Verify that the leave type was updated and the result is of type Unit.
        result.ShouldBeOfType<Unit>();
        _mockRepo.Verify(r => r.UpdateAsync(It.Is<LeaveType>(lt => lt.Id == 1 && lt.Name == "Updated Annual Leave" && lt.DefaultDays == 15)), Times.Once);
    }

    /// <summary>
    /// Tests the UpdateLeaveTypeCommandHandler with invalid data.
    /// Ensures that a BadRequestException is thrown when the command contains invalid data.
    /// </summary>
    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowBadRequestException()
    {
        // Arrange: Create an instance of the command handler and an invalid update command.
        var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _logger.Object);
        var command = new UpdateLeaveTypeCommand
        {
            Id = 1,
            Name = "", // Invalid: Name is required.
            DefaultDays = -5 // Invalid: DefaultDays must be greater than 0.
        };

        // Act & Assert: Verify that a BadRequestException is thrown for invalid data.
        await Should.ThrowAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
    }
}