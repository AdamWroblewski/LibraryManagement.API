using FluentValidation;
using LibraryManagement.Application.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, problemDetails) = exception switch
        {
            ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                CreateValidationProblemDetails(httpContext, validationEx)
            ),

            InvalidOperationException => (
                StatusCodes.Status400BadRequest,
                CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status400BadRequest,
                    "Bad Request",
                    exception.Message)
            ),

            UserRegistrationFailedException regEx => (
                StatusCodes.Status400BadRequest,
                CreateProblemDetails(httpContext, StatusCodes.Status400BadRequest, "Registration Failed", string.Join("; ", regEx.Errors))
            ),

            InvalidCredentialsException => (
                StatusCodes.Status401Unauthorized,
                CreateProblemDetails(httpContext, StatusCodes.Status401Unauthorized, "Authentication Failed", exception.Message)
            ),

            EntityNotFoundException or KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status404NotFound,
                    "Resource Not Found",
                    exception.Message)
            ),

            DuplicateResourceException => (
                StatusCodes.Status409Conflict,
                CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status409Conflict,
                    "Conflict",
                    exception.Message)
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status500InternalServerError,
                    "Server Error",
                    "An unexpected error occurred.")
            )
        };

        // Log appropriately based on severity
        if (statusCode >= 500)
        {
            _logger.LogError(exception, "Unhandled exception occurred for path {Path}", httpContext.Request.Path);
        }
        else
        {
            _logger.LogWarning(exception, "Handled exception occurred for path {Path}", httpContext.Request.Path);
        }

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType(), cancellationToken);

        return true; // Mark as handled
    }

    private static ProblemDetails CreateProblemDetails(HttpContext context, int status, string title, string detail)
    {
        return new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
    }

    private static ValidationProblemDetails CreateValidationProblemDetails(HttpContext context, ValidationException ex)
    {
        var errors = ex.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        var problemDetails = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "One or more validation errors occurred.",
            Instance = context.Request.Path
        };

        return problemDetails;
    }
}