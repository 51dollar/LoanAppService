using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace LoanService.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(
                configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
                name: "sqlserver",
                timeout: TimeSpan.FromSeconds(30),
                tags: new[] { "ok" }
            );

        return services;
    }

    public static IEndpointRouteBuilder MapAppHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/health");
        endpoints.MapHealthChecks("/health/ok", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ok")
        });

        return endpoints;
    }
}