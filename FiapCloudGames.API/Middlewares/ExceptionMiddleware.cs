using FiapCloudGames.Domain.Exceptions;

namespace FiapCloudGames.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context) {
        try {
            await this._next(context);
        } catch (Exception ex) {
            this._logger.LogError(ex, "Erro interno no servidor.");

            context.Response.StatusCode = ex switch {
                UserNotFoundException => StatusCodes.Status401Unauthorized,
                InvalidFormException => StatusCodes.Status400BadRequest,
                EmailAlreadyInUseException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            this._logger.LogError("{Message}", ex.Message);
        }
    }
}
