using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TradingDay.Data.Config;
using Xunit;

namespace TradingDay.Tests.Config;

public sealed class AlpacaConfigTests
{
    [Fact]
    public void AlpacaConfig_BindsFromConfiguration_WhenSectionIsPopulated()
    {
        // Arrange
        var inMemory = new Dictionary<string, string?>
        {
            [$"{AlpacaConfig.SectionName}:{nameof(AlpacaConfig.KeyId)}"] = "test-key-id",
            [$"{AlpacaConfig.SectionName}:{nameof(AlpacaConfig.SecretKey)}"] = "test-secret",
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemory)
            .Build();

        var services = new ServiceCollection();
        services.Configure<AlpacaConfig>(configuration.GetSection(AlpacaConfig.SectionName));
        var provider = services.BuildServiceProvider();

        // Act
        var options = provider.GetRequiredService<IOptions<AlpacaConfig>>().Value;

        // Assert
        Assert.Equal("test-key-id", options.KeyId);
        Assert.Equal("test-secret", options.SecretKey);
    }

    [Fact]
    public void AlpacaConfig_SectionName_IsAlpacaConfig()
    {
        Assert.Equal("AlpacaConfig", AlpacaConfig.SectionName);
    }

    [Fact]
    public void AlpacaConfig_HeaderConstants_HaveExpectedValues()
    {
        Assert.Equal("APCA-API-KEY-ID", AlpacaConfig.ALPACA_API_KEY_ENV);
        Assert.Equal("APCA-API-SECRET-KEY", AlpacaConfig.ALPACA_API_SECRET_ENV);
    }

    [Fact]
    public void AlpacaConfig_DefaultKeyId_IsEmptyString()
    {
        var config = new AlpacaConfig();
        Assert.Equal(string.Empty, config.KeyId);
        Assert.Equal(string.Empty, config.SecretKey);
    }

    [Fact]
    public void AlpacaConfig_FromEnvironment_ThrowsWhenKeyIdNotSet()
    {
        // Ensure the env var is absent for this test
        var originalKey = Environment.GetEnvironmentVariable(AlpacaConfig.ALPACA_API_KEY_ENV);
        var originalSecret = Environment.GetEnvironmentVariable(AlpacaConfig.ALPACA_API_SECRET_ENV);

        try
        {
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_KEY_ENV, null);
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_SECRET_ENV, null);

            var ex = Assert.Throws<InvalidOperationException>(() => AlpacaConfig.FromEnvironment());
            Assert.Contains(AlpacaConfig.ALPACA_API_KEY_ENV, ex.Message);
        }
        finally
        {
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_KEY_ENV, originalKey);
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_SECRET_ENV, originalSecret);
        }
    }

    [Fact]
    public void AlpacaConfig_FromEnvironment_ReturnsPopulatedConfig_WhenEnvVarsSet()
    {
        var originalKey = Environment.GetEnvironmentVariable(AlpacaConfig.ALPACA_API_KEY_ENV);
        var originalSecret = Environment.GetEnvironmentVariable(AlpacaConfig.ALPACA_API_SECRET_ENV);

        try
        {
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_KEY_ENV, "env-key");
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_SECRET_ENV, "env-secret");

            var config = AlpacaConfig.FromEnvironment();

            Assert.Equal("env-key", config.KeyId);
            Assert.Equal("env-secret", config.SecretKey);
        }
        finally
        {
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_KEY_ENV, originalKey);
            Environment.SetEnvironmentVariable(AlpacaConfig.ALPACA_API_SECRET_ENV, originalSecret);
        }
    }
}
