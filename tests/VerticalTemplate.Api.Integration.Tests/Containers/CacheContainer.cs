using System.Diagnostics.CodeAnalysis;
using DotNet.Testcontainers.Builders;

namespace VerticalTemplate.Api.Integration.Tests.Containers;

[ExcludeFromCodeCoverage]
internal sealed class CacheContainer : BaseContainer<CacheContainer>
{
    private const ushort CacheDefaultPort = 6379;

    public string GetCacheConnectionString() => $"{_container!.Hostname}:{_container.GetMappedPublicPort(CacheDefaultPort)}";

    protected override IContainer BuildContainer()
    {
        return new ContainerBuilder()
           .WithImage("redis:latest")
           .WithPortBinding(CacheDefaultPort, true)
           .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(CacheDefaultPort))
           .Build();
    }

    public override string GetConnectionString() =>
        $"{_container!.Hostname}:{_container.GetMappedPublicPort(CacheDefaultPort)}";
}