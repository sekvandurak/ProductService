using ErrorOr;

namespace ProductService.Api.Common.Errors;

public static class ErrorMapping
{
    public static int ToStatusCode(ErrorType type) =>
        type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
}
