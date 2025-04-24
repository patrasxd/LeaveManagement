using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Models;

/// <summary>
/// A custom implementation of <see cref="ProblemDetails"/> to include additional error details.
/// </summary>
public class CustomProblemDetails : ProblemDetails
{
    /// <summary>
    /// A dictionary containing validation errors or additional error details.
    /// The key represents the field or property, and the value is an array of error messages.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}