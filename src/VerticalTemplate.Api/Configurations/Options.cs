using VerticalTemplate.Api.Common.Settings;

namespace VerticalTemplate.Api.Configurations;

internal static class Options
{
    internal static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppSettings>(config);
    }
}