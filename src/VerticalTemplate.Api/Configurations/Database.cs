﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VerticalTemplate.Api.Infrastructure.Persistance;
using VerticalTemplate.Api.Infrastructure.Persistance.Interceptors;

namespace VerticalTemplate.Api.Configurations;

[ExcludeFromCodeCoverage]
internal static class Database
{
    internal static void SetupDatabase(this IHostApplicationBuilder builder)
    {       
        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        builder.Services.SetupRepositories();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"))
                .EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
        }, ServiceLifetime.Scoped);

        builder.EnrichSqlServerDbContext<ApplicationDbContext>();
    }

    private static void SetupRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<IApplicationDbContext>()
                                    .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());
    }
}
