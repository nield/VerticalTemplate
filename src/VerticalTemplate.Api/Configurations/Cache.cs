namespace VerticalTemplate.Api.Configurations;

internal static class Cache
{
    internal static void ConfigureCache(this IHostApplicationBuilder builder)
    {
        builder.AddRedisDistributedCache("Redis");
    }
}
