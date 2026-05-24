namespace TradingDay.Core.Models;

/// <summary>Represents the broker's response for a placed or queried order.</summary>
/// <param name="OrderId">The broker-assigned unique identifier for the order.</param>
/// <param name="Symbol">The ticker symbol of the order.</param>
/// <param name="Side">Whether the order was a buy or sell.</param>
/// <param name="Type">The execution type of the order.</param>
/// <param name="Status">The current lifecycle state of the order.</param>
/// <param name="FilledQuantity">The number of shares filled so far. Zero if not yet filled.</param>
/// <param name="FilledAveragePrice">
/// The average fill price. <see langword="null"/> if no fills have occurred.
/// </param>
/// <param name="SubmittedAt">The timestamp when the order was submitted to the broker.</param>
public record OrderResult(
    string OrderId,
    string Symbol,
    OrderSide Side,
    OrderType Type,
    OrderStatus Status,
    decimal FilledQuantity,
    decimal? FilledAveragePrice,
    DateTimeOffset SubmittedAt);
