using FastEndpoints.Swagger;

namespace VerticalTemplate.Api.Configurations;

internal static class FastEndpoints
{
    internal static void ConfigureFastEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints(options =>
        {
            options.IncludeAbstractValidators = true;
        })
        .SwaggerDocument(opt =>
        {
            opt.MaxEndpointVersion = 1;
            opt.DocumentSettings = s =>
            {
                s.Version = "v1";
                s.Title = "API";
                s.Description = "V1";
            };
        });
    }
}
