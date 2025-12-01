using LoanService.Database;
using LoanService.Database.Repository;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Extensions;

public static class DbExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddDbContext<LoanDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        return services;
    }
}