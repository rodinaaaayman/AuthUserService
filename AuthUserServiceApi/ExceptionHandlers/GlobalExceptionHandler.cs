using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AuthUserServiceApi.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Type = "https://httpstatuses.com/500",
                Detail = exception.Message
            };

            context.Response.StatusCode = problem.Status.Value;

            await context.Response.WriteAsJsonAsync(problem);

            return true;
        }
    }
}
