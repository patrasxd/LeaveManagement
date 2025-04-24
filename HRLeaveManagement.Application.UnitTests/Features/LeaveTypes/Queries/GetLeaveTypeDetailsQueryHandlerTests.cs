using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HRLeaveManagement.Application.MappingProfiles;
using HRLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HRLeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

/// <summary>
/// Unit tests for the GetLeaveTypeDetailsQueryHandler.
/// </summary>
public class GetLeaveTypeDetailsQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>> _logger;

    /// <summary>
    /// Initializes the test class by setting up the mock repository, AutoMapper, and logger.
    /// </summary>
    public GetLeaveTypeDetailsQueryHandlerTests()
    {
        // Set up a mock repository for ILeaveTypeRepository with predefined data.
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        // Configure AutoMapper with the LeaveTypeProfile mapping profile.
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        // Initialize a mock logger for the query handler.
        _logger = new Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>>();
    }

    /// <summary>
    /// Tests the GetLeaveTypeDetailsQueryHandler with a valid ID.
    /// Ensures that the correct LeaveTypeDetailsDto is returned.
    /// </summary>
    [Fact]
    public async Task GetLeaveTypeDetailsTest_ValidId_ReturnsCorrectLeaveType()
    {
        // Arrange: Create an instance of the query handler and a query with an existing ID.
        var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _logger.Object);
        var query = new GetLeaveTypeDetailsQuery(1);

        // Act: Execute the query handler.
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert: Verify the result matches the expected LeaveTypeDetailsDto.
        result.ShouldBeOfType<LeaveTypeDetailsDto>();
        result.Id.ShouldBe(1);
        result.Name.ShouldBe("TEST Annual Leave");
        result.DefaultDays.ShouldBe(20);
    }

    /// <summary>
    /// Tests the GetLeaveTypeDetailsQueryHandler with an invalid ID.
    /// Ensures that null is returned when the leave type does not exist.
    /// </summary>
    [Fact]
    public async Task GetLeaveTypeDetailsTest_InvalidId_ReturnsNull()
    {
        // Arrange: Create an instance of the query handler and a query with a non-existent ID.
        var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object, _logger.Object);
        var query = new GetLeaveTypeDetailsQuery(999); // ID not present in the mock data.

        // Act: Execute the query handler.
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert: Verify the result is null for an invalid ID.
        result.ShouldBeNull();
    }
}