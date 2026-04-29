namespace eCommerceProducts.Api.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger
)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.GetType().Name}: {ex.Message}");

            if (ex.InnerException is not null)
            {
                _logger.LogError(
                    $"Inner Exception: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}"
                );
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Type = ex.GetType().Name,
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

/// <summary>
/// Extension method to add the ExceptionHandlingMiddleware to the application's request pipeline.
/// </summary> <param name="app">The application builder to add the middleware to.</param>
/// <returns>The updated application builder.</returns>
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(
        this IApplicationBuilder app
    ) => app.UseMiddleware<ExceptionHandlingMiddleware>();
}
