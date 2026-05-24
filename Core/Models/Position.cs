namespace TradingDay.Core.Models;

/// <summary>Represents an open position held in the trading account.</summary>
/// <param name="Symbol">The ticker symbol of the held asset.</param>
/// <param name="Quantity">
/// The signed quantity of shares held.
/// Positive values indicate a long position; negative values indicate a short position.
/// </param>
/// <param name="AverageEntryPrice">The average price paid (or received) when the position was opened.</param>
/// <param name="CurrentMarketValue">The current market value of the entire position in account currency.</param>
public record Position(
    string Symbol,
    decimal Quantity,
    decimal AverageEntryPrice,
    decimal CurrentMarketValue);
