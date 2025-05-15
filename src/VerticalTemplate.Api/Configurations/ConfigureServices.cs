using VerticalTemplate.Api.Common.Services;

namespace VerticalTemplate.Api.Configurations;

public static class ConfigureServices
{
    public static IHostApplicationBuilder ConfigureApiServices(this IHostApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureSettings(config);

        builder.Services.ConfigureFastEndpoints();

        builder.SetupDatabase();

        return builder;
    }

    
}
