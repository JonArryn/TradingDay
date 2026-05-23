namespace TradingDay.Data.Config;

/// <summary>
/// Configuration options for the Alpaca Markets API.
/// Supports binding from <see cref="Microsoft.Extensions.Configuration.IConfiguration"/>
/// via the Options pattern (section name: <see cref="SectionName"/>), as well as
/// direct construction from environment variables via <see cref="FromEnvironment"/>.
/// </summary>
public sealed class AlpacaConfig
{
    /// <summary>
    /// The configuration section name used when binding via <c>IOptions&lt;AlpacaConfig&gt;</c>.
    /// Supply values as environment variables using the ASP.NET Core double-underscore convention:
    /// <c>AlpacaConfig__KeyId</c> and <c>AlpacaConfig__SecretKey</c>.
    /// </summary>
    public const string SectionName = "AlpacaConfig";

    /// <summary>The Alpaca API key header name and legacy environment variable name.</summary>
    public const string ALPACA_API_KEY_ENV = "APCA-API-KEY-ID";

    /// <summary>The Alpaca API secret header name and legacy environment variable name.</summary>
    public const string ALPACA_API_SECRET_ENV = "APCA-API-SECRET-KEY";

    /// <summary>Gets or sets the Alpaca API key ID.</summary>
    public string KeyId { get; set; } = string.Empty;

    /// <summary>Gets or sets the Alpaca API secret key.</summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Constructs an <see cref="AlpacaConfig"/> by reading the legacy
    /// <c>APCA-API-KEY-ID</c> and <c>APCA-API-SECRET-KEY</c> environment variables directly.
    /// Prefer the Options pattern (<see cref="SectionName"/>) for new code.
    /// </summary>
    /// <returns>A populated <see cref="AlpacaConfig"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a required environment variable is not set.</exception>
    public static AlpacaConfig FromEnvironment() => new()
    {
        KeyId = Environment.GetEnvironmentVariable(ALPACA_API_KEY_ENV)
                ?? throw new InvalidOperationException($"{ALPACA_API_KEY_ENV} not set"),
        SecretKey = Environment.GetEnvironmentVariable(ALPACA_API_SECRET_ENV)
                    ?? throw new InvalidOperationException($"{ALPACA_API_SECRET_ENV} not set"),
    };
}
