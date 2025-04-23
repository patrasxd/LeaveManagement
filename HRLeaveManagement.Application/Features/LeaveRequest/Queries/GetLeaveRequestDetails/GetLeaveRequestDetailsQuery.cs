using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailsQuery : IRequest<LeaveRequestDetailsDto>
{
    public int Id { get; set; }
}