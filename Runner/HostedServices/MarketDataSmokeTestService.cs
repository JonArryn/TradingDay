using TradingDay.Core.Interfaces;

namespace TradingDay.Runner.HostedServices;

/// <summary>
/// A one-shot hosted service that fetches AAPL bars on startup to verify that
/// <see cref="IMarketDataProvider"/> is correctly resolved from the DI container
/// and can reach the Alpaca API.
/// </summary>
public sealed class MarketDataSmokeTestService : BackgroundService
{
    private readonly IMarketDataProvider _marketDataProvider;
    private readonly ILogger<MarketDataSmokeTestService> _logger;

    /// <summary>
    /// Initialises a new instance of <see cref="MarketDataSmokeTestService"/>.
    /// </summary>
    /// <param name="marketDataProvider">The market data provider resolved from DI.</param>
    /// <param name="logger">The logger.</param>
    public MarketDataSmokeTestService(
        IMarketDataProvider marketDataProvider,
        ILogger<MarketDataSmokeTestService> logger)
    {
        _marketDataProvider = marketDataProvider;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string symbol = "AAPL";

        var easternZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
        var yesterday = DateTimeOffset.UtcNow
            .ToOffset(easternZone.GetUtcOffset(DateTimeOffset.UtcNow.UtcDateTime))
            .Date.AddDays(-2);
        var from = new DateTimeOffset(yesterday.AddHours(9).AddMinutes(30), easternZone.GetUtcOffset(yesterday));
        var to = new DateTimeOffset(yesterday.AddHours(16), easternZone.GetUtcOffset(yesterday));

        _logger.LogInformation("Smoke test: fetching daily bars for {Symbol} ({From:yyyy-MM-dd} -> {To:yyyy-MM-dd})",
            symbol, from, to);

        try
        {
            var bars = await _marketDataProvider.GetBarsAsync(symbol, from, to, stoppingToken);

            if (bars.Count == 0)
            {
                _logger.LogWarning("Smoke test: no bars returned for {Symbol}.", symbol);
                return;
            }

            _logger.LogInformation("Smoke test: {Count} bars returned for {Symbol}.", bars.Count, symbol);

            foreach (var bar in bars)
            {
                _logger.LogInformation(
                    "  {Date:yyyy-MM-dd}  O={Open:F2}  H={High:F2}  L={Low:F2}  C={Close:F2}  V={Volume:N0}",
                    bar.Timestamp, bar.Open, bar.High, bar.Low, bar.Close, bar.Volume);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Smoke test: failed to fetch bars for {Symbol}.", symbol);
        }
    }
}
