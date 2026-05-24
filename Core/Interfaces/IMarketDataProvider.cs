using TradingDay.Core.Models;

namespace TradingDay.Core.Interfaces;

/// <summary>
/// Provider-agnostic abstraction for reading market data.
/// Concrete implementations live in <c>TradingDay.Data</c>.
/// </summary>
public interface IMarketDataProvider
{
    /// <summary>Returns historical OHLCV bars for a symbol within the given time range.</summary>
    /// <param name="symbol">The ticker symbol (e.g. <c>"AAPL"</c>).</param>
    /// <param name="from">The inclusive start of the requested range (UTC).</param>
    /// <param name="to">The inclusive end of the requested range (UTC).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A read-only list of <see cref="Bar"/> records in ascending time order.</returns>
    Task<IReadOnlyList<Bar>> GetBarsAsync(
        string symbol,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken = default);

    /// <summary>Returns the latest bid/ask quote for a symbol.</summary>
    /// <param name="symbol">The ticker symbol (e.g. <c>"AAPL"</c>).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A <see cref="Quote"/> with the current best bid and ask.</returns>
    Task<Quote> GetLatestQuoteAsync(string symbol, CancellationToken ct);
}
