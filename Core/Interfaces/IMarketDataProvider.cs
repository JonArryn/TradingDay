using TradingDay.Core.Models;

namespace TradingDay.Core.Interfaces;

public interface IMarketDataProvider
{
    Task<IReadOnlyList<Bar>> GetBarsAsync(
        string symbol,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken = default);
}