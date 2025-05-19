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
            opt.DocumentSettings = s =>
            {                
                s.Version = "v1";
                s.Title = "API";
                s.Description = "API Template";
            };
        });
    }
}
