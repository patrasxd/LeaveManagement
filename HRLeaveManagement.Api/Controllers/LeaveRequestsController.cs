using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveRequestsController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
    {
        // get leave requests
        var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());

        // return leave requests
        return Ok(leaveRequests);
    }

    // GET: api/<LeaveRequestsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        // get leave request details
        var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailsQuery
        {
            Id = id
        });

        // return leave requests details
        return Ok(leaveRequest);
    }

    // POST api/<LeaveRequestsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequest)
    {
        // create leave request
        var response = await _mediator.Send(leaveRequest);

        // return created leave request
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveRequestsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
    {
        // update leave request
        var response = await _mediator.Send(leaveRequest);

        // return no content
        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/CancelRequest
    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
    {
        // cancel leave request
        var response = await _mediator.Send(cancelLeaveRequest);

        // return no content
        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/UpdateApproval
    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
    {
        // update leave request
        var response = await _mediator.Send(updateApprovalRequest);

        // return no content
        return NoContent();
    }

    // DELETE api/<LeaveRequestsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        // create command to delete leave request
        var command = new DeleteLeaveRequestCommand { Id = id };

        // delete leave request
        var response = await _mediator.Send(command);

        // return no content
        return NoContent();
    }
}
