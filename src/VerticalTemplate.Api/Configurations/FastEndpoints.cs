using System.Diagnostics.CodeAnalysis;
using FastEndpoints.Swagger;

namespace VerticalTemplate.Api.Configurations;

[ExcludeFromCodeCoverage]
internal static class FastEndpoints
{
    internal static void ConfigureFastEndpoints(this IServiceCollection services)
    {
        services.AddFastEndpoints()
        .SwaggerDocument(opt =>
        {
            opt.MaxEndpointVersion = 1;
            opt.MinEndpointVersion = 1;
            opt.DocumentSettings = s =>
            {
                s.Version = "v1";
                s.DocumentName = "v1";
                s.Title = "API";
                s.Description = "API Template";
            };
        })
        .SwaggerDocument(opt =>
        {
            opt.MaxEndpointVersion = 2;
            opt.MinEndpointVersion = 2;
            opt.DocumentSettings = s =>
            {
                s.Version = "v2";
                s.DocumentName = "v2";
                s.Title = "API";
                s.Description = "API Template";
            };
        });
    }
}
