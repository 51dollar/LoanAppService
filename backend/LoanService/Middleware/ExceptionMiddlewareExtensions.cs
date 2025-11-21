using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace LoanService.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionHandlerFeature?.Error == null)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var fallback = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
                    await context.Response.WriteAsync(fallback);
                    return;
                }

                var error = exceptionHandlerFeature.Error;

                context.Response.StatusCode = error switch
                {
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                var result = JsonSerializer.Serialize(new
                {
                    error = error.Message,
                    type = error.GetType().Name
                });

                await context.Response.WriteAsync(result);
            });
        });

        return app;
    }
}