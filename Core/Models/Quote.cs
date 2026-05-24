namespace TradingDay.Core.Models;

/// <summary>The latest bid/ask quote for a symbol.</summary>
/// <param name="Symbol">The ticker symbol this quote is for.</param>
/// <param name="AskPrice">The current best ask (offer) price.</param>
/// <param name="BidPrice">The current best bid price.</param>
/// <param name="AskSize">The number of shares available at the ask price.</param>
/// <param name="BidSize">The number of shares available at the bid price.</param>
/// <param name="Timestamp">The time at which this quote was recorded.</param>
public record Quote(
    string Symbol,
    decimal AskPrice,
    decimal BidPrice,
    decimal AskSize,
    decimal BidSize,
    DateTimeOffset Timestamp);
