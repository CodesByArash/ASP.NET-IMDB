using Microsoft.AspNetCore.Mvc;

namespace api.Helpers;

public static class ApiResponse
{
    public static IActionResult Success<T>(T data, string message = "Success")
    {
        return new OkObjectResult(new
        {
            success = true,
            message = message,
            data = data,
            timestamp = DateTime.UtcNow
        });
    }

    public static IActionResult Error(string message, int statusCode = 400)
    {
        return new ObjectResult(new
        {
            success = false,
            message = message,
            timestamp = DateTime.UtcNow
        })
        {
            StatusCode = statusCode
        };
    }

    public static IActionResult NotFound(string message = "Resource not found")
    {
        return new NotFoundObjectResult(new
        {
            success = false,
            message = message,
            timestamp = DateTime.UtcNow
        });
    }

    public static IActionResult Unauthorized(string message = "Unauthorized access")
    {
        return new UnauthorizedObjectResult(new
        {
            success = false,
            message = message,
            timestamp = DateTime.UtcNow
        });
    }

    public static IActionResult BadRequest(string message = "Bad request")
    {
        return new BadRequestObjectResult(new
        {
            success = false,
            message = message,
            timestamp = DateTime.UtcNow
        });
    }
}
