using LoanService.Database;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LoanDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        logger.LogInformation("Начинаю применение миграций...");
        
        try
        {
            await db.Database.MigrateAsync();
            
            logger.LogInformation("Миграции успешно применены.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при применении миграций");
            throw;
        }
    }
}