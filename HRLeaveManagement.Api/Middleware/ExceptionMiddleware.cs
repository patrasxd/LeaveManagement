using HRLeaveManagement.Api.Models;
using HRLeaveManagement.Application.Exceptions;
using System.Net;

namespace HRLeaveManagement.Api.Middleware;

/// <summary>
/// Middleware to handle exceptions globally in the application.
/// Converts exceptions into a standardized problem details response.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Middleware invocation method.
    /// Catches exceptions thrown by the next middleware and handles them.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            // Pass the request to the next middleware in the pipeline.
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during request processing.
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Handles exceptions by creating a standardized problem details response.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="ex">The exception to handle.</param>
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError; // Default status code.
        CustomProblemDetails problem = new(); // Problem details object to return.

        // Determine the type of exception and set the appropriate response details.
        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors, // Validation errors for bad requests.
                };
                break;

            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = notFoundException.InnerException?.Message,
                };
                break;

            default:
                // Handle all other exceptions as internal server errors.
                problem = new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = ex.StackTrace, // Include stack trace for debugging.
                };
                break;
        }

        // Set the response status code and write the problem details as JSON.
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}