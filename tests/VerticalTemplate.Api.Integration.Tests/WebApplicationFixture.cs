using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System.Net.Http.Headers;
using VerticalTemplate.Api.Common.Constants;
using VerticalTemplate.Api.Infrastructure.Persistance;
using VerticalTemplate.Api.Integration.Tests.Containers;

namespace VerticalTemplate.Api.Integration.Tests;

public class WebApplicationFixture : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory = new();

    private string? _databaseConnectionString = null;
    private Respawner? _respawner = null;
    private HttpClient? _httpClient = null;

    public HttpClient HttpClient
    {
        get
        {
            if (_httpClient is null)
            {
                throw new NullReferenceException("HttpClient not set");
            }

            return _httpClient;
        }
    }

    public async Task InitializeAsync()
    {
        await StartContainers();

        _httpClient = _factory.CreateClient();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(EnvironmentConstants.Test);

        _databaseConnectionString = DatabaseContainer.Instance.GetConnectionString();

        _respawner = await Respawner.CreateAsync(_databaseConnectionString, new RespawnerOptions
        {
            TablesToIgnore = [ApplicationDbContext.MigrationTableName],
            WithReseed = true
        });
    }

    private static async Task StartContainers()
    {
        try
        {
            using var cancellationSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            await Task.WhenAll(
                DatabaseContainer.Instance.StartContainerAsync(cancellationSource.Token),
                CacheContainer.Instance.StartContainerAsync(cancellationSource.Token));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task ResetDatabaseAsync()
    {
        if (_respawner is not null)
        {
            await _respawner.ResetAsync(_databaseConnectionString!);
        }

        using var scope = _factory.Services.CreateScope();

        var dbContextInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await dbContextInitialiser.MigrateDatabaseAsync();
        await dbContextInitialiser.SeedDataAsync();
    }

    public Task DisposeAsync()
    {
        _httpClient?.Dispose();

        return Task.CompletedTask;
    }
}
