using System.Diagnostics.CodeAnalysis;
using VerticalTemplate.Api.Common.Settings;

namespace VerticalTemplate.Api.Configurations;

[ExcludeFromCodeCoverage]
internal static class Options
{
    internal static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppSettings>(config);
    }
}