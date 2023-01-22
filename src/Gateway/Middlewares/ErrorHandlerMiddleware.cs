using System.Net;
using System.Text.Json;
using SharedKernel.Exceptions;

namespace Gateway.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message + "\\n" + error.StackTrace);
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                ConflictException => (int)HttpStatusCode.Conflict,
                NotFoundException => (int)HttpStatusCode.NotFound,
                not null => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { message = error?.InnerException?.Message ?? "Error" });
            await response.WriteAsync(result);
        }
    }
}