using System.Diagnostics.CodeAnalysis;
using VerticalTemplate.Api.Common.Services;

namespace VerticalTemplate.Api.Configurations;

[ExcludeFromCodeCoverage]
internal static class ConfigureServices
{
    internal static IHostApplicationBuilder ConfigureApiServices(this IHostApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureSettings(config);

        builder.Services.ConfigureFastEndpoints();

        builder.SetupDatabase();

        builder.ConfigureCache();

        return builder;
    }    
}
