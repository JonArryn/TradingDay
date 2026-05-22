namespace TradingDay.Data.Config;

public sealed record AlpacaConfig(string KeyId, string SecretKey)
{
    public const string ALPACA_API_KEY_ENV = "APCA-API-KEY-ID";
    public const string ALPACA_API_SECRET_ENV = "APCA-API-SECRET-KEY";
    public static AlpacaConfig FromEnvironment() => new(
        Environment.GetEnvironmentVariable(ALPACA_API_KEY_ENV) ??
        throw new InvalidOperationException($"{ALPACA_API_KEY_ENV} not set"),
        Environment.GetEnvironmentVariable(ALPACA_API_SECRET_ENV) ??
        throw new InvalidOperationException($"{ALPACA_API_SECRET_ENV} not set")
    );
}