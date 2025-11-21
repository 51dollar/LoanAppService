namespace LoanService.Service;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILoanService, LoanService>();
        
        return services;
    }
}