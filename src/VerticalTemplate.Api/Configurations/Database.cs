using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VerticalTemplate.Api.Infrastructure.Persistance;
using VerticalTemplate.Api.Infrastructure.Persistance.Interceptors;

namespace VerticalTemplate.Api.Configurations;

internal static class Database
{
    internal static void SetupDatabase(this IHostApplicationBuilder builder)
    {       
        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"))
                .EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
        }, ServiceLifetime.Scoped);

        builder.EnrichSqlServerDbContext<ApplicationDbContext>();
    }
}
