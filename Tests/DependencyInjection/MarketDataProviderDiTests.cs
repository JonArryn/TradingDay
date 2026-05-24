using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TradingDay.Core.Interfaces;
using TradingDay.Data.Config;
using TradingDay.Data.Http;
using Xunit;

namespace TradingDay.Tests.DependencyInjection;

/// <summary>
/// Verifies that <see cref="IMarketDataProvider"/> resolves from the DI container
/// when wired using the same registrations as <c>Program.cs</c>.
/// </summary>
public sealed class MarketDataProviderDiTests
{
    [Fact]
    public void IMarketDataProvider_ResolvesFromDi_AsAlpacaDataProvider()
    {
        var inMemory = new Dictionary<string, string?>
        {
            [$"{AlpacaConfig.SectionName}:{nameof(AlpacaConfig.KeyId)}"] = "test-key",
            [$"{AlpacaConfig.SectionName}:{nameof(AlpacaConfig.SecretKey)}"] = "test-secret",
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemory)
            .Build();

        var services = new ServiceCollection();
        services.Configure<AlpacaConfig>(configuration.GetSection(AlpacaConfig.SectionName));
        services.AddHttpClient<IMarketDataProvider, AlpacaDataProvider>((sp, client) =>
        {
            var cfg = sp.GetRequiredService<IOptions<AlpacaConfig>>().Value;
            client.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_KEY_ENV, cfg.KeyId);
            client.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_SECRET_ENV, cfg.SecretKey);
        });

        var provider = services.BuildServiceProvider();

        var marketDataProvider = provider.GetRequiredService<IMarketDataProvider>();

        Assert.NotNull(marketDataProvider);
        Assert.IsType<AlpacaDataProvider>(marketDataProvider);
    }

    [Fact]
    public void AlpacaDataProvider_HttpClient_HasAuthHeadersFromOptions()
    {
        var inMemory = new Dictionary<string, string?>
        {
            [$"{AlpacaConfig.SectionName}:{nameof(AlpacaConfig.KeyId)}"] = "expected-key",
            [$"{AlpacaConfig.SectionName}:{nameof(AlpacaConfig.SecretKey)}"] = "expected-secret",
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemory)
            .Build();

        var services = new ServiceCollection();
        services.Configure<AlpacaConfig>(configuration.GetSection(AlpacaConfig.SectionName));

        HttpClient? capturedClient = null;
        services.AddHttpClient<IMarketDataProvider, AlpacaDataProvider>((sp, client) =>
        {
            var cfg = sp.GetRequiredService<IOptions<AlpacaConfig>>().Value;
            client.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_KEY_ENV, cfg.KeyId);
            client.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_SECRET_ENV, cfg.SecretKey);
            capturedClient = client;
        });

        var provider = services.BuildServiceProvider();
        _ = provider.GetRequiredService<IMarketDataProvider>();

        Assert.NotNull(capturedClient);
        Assert.True(capturedClient!.DefaultRequestHeaders.TryGetValues(AlpacaConfig.ALPACA_API_KEY_ENV, out var keyValues));
        Assert.Equal("expected-key", keyValues!.Single());
        Assert.True(capturedClient.DefaultRequestHeaders.TryGetValues(AlpacaConfig.ALPACA_API_SECRET_ENV, out var secretValues));
        Assert.Equal("expected-secret", secretValues!.Single());
    }
}
