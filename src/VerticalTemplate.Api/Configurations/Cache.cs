using System.Diagnostics.CodeAnalysis;

namespace VerticalTemplate.Api.Configurations;

[ExcludeFromCodeCoverage]
internal static class Cache
{
    internal static void ConfigureCache(this IHostApplicationBuilder builder)
    {
        builder.AddRedisDistributedCache("Redis");
    }
}
