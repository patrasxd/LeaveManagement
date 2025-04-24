using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.MappingProfiles;
using HRLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HRLeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

/// <summary>
/// Unit tests for the GetLeaveTypesQueryHandler class.
/// </summary>
public class GetLeaveTypesQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _logger;

    /// <summary>
    /// Initializes the test class by setting up mock dependencies and the AutoMapper configuration.
    /// </summary>
    public GetLeaveTypesQueryHandlerTests()
    {
        // Mock the LeaveType repository.
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        // Configure AutoMapper with the LeaveTypeProfile mapping profile.
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        // Mock the application logger for the query handler.
        _logger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    /// <summary>
    /// Tests the GetLeaveTypesQueryHandler to ensure it returns the correct list of LeaveTypeDto objects.
    /// </summary>
    [Fact]
    public async Task GetLeaveTypesTest()
    {
        // Arrange: Create an instance of the query handler with mocked dependencies.
        var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _logger.Object);

        // Act: Call the Handle method with a GetLeaveTypesQuery.
        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        // Assert: Verify the result is a list of LeaveTypeDto and contains the expected number of items.
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}