namespace LoanService.Extensions;

public static class CorsExtensions
{
    private const string FrontendCorsPolicy = "AllowFrontend";
    
    public static IServiceCollection AddFrontendCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(FrontendCorsPolicy, policy =>
            {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
    
    public static IApplicationBuilder UseFrontendCors(this IApplicationBuilder app)
    {
        app.UseCors(FrontendCorsPolicy);
        return app;
    }
}