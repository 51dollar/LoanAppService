using LoanService.Service;

namespace LoanService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILoanService, Service.LoanService>();
        
        return services;
    }
}