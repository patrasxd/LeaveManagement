using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HRLeaveManagement.Application.UnitTests.Mocks;
using HRLeaveManagement.Domain;
using Moq;
using Shouldly;
using MediatR;

namespace HRLeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands;

/// <summary>
/// Unit tests for the DeleteLeaveTypeCommandHandler.
/// </summary>
public class DeleteLeaveTypeCommandHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    /// <summary>
    /// Initializes the test class by setting up the mock repository.
    /// </summary>
    public DeleteLeaveTypeCommandHandlerTests()
    {
        // Set up a mock repository for ILeaveTypeRepository.
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
    }

    /// <summary>
    /// Tests the DeleteLeaveTypeCommandHandler with a valid ID.
    /// Ensures that the leave type is successfully deleted from the repository.
    /// </summary>
    [Fact]
    public async Task Handle_ValidId_ShouldDeleteLeaveType()
    {
        // Arrange: Create an instance of the command handler and a valid delete command.
        var handler = new DeleteLeaveTypeCommandHandler(_mockRepo.Object);
        var command = new DeleteLeaveTypeCommand { Id = 1 };

        // Act: Handle the delete command.
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert: Verify that the leave type was deleted and the result is of type Unit.
        result.ShouldBeOfType<Unit>();
        _mockRepo.Verify(r => r.DeleteAsync(It.Is<LeaveType>(lt => lt.Id == 1)), Times.Once);
    }

    /// <summary>
    /// Tests the DeleteLeaveTypeCommandHandler with an invalid ID.
    /// Ensures that a NotFoundException is thrown when the leave type does not exist.
    /// </summary>
    [Fact]
    public async Task Handle_InvalidId_ShouldThrowNotFoundException()
    {
        // Arrange: Create an instance of the command handler and an invalid delete command.
        var handler = new DeleteLeaveTypeCommandHandler(_mockRepo.Object);
        var command = new DeleteLeaveTypeCommand { Id = 999 }; // ID not present in the mock repository.

        // Act & Assert: Verify that a NotFoundException is thrown for the invalid ID.
        await Should.ThrowAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
    }
}