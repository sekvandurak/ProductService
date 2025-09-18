using System.Net;
using System.Text.Json;

namespace ProductService.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error occuredd.");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "An unexpected error occured. Plesae try again.",
                detail = ex.Message

            };
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
    
    
}