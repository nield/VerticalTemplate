﻿using VerticalTemplate.Api.Infrastructure.Persistance;

namespace VerticalTemplate.Api.Configurations;

public static class Migrations
{
    public static async Task ApplyMigrations(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await dbContextInitialiser.MigrateDatabaseAsync();

        await dbContextInitialiser.SeedDataAsync();
    }
}
