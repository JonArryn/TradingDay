namespace TradingDay.Core.Models;

/// <summary>Describes an order to be submitted to a broker.</summary>
/// <param name="Symbol">The ticker symbol to trade (e.g. <c>"AAPL"</c>).</param>
/// <param name="Side">Whether to buy or sell.</param>
/// <param name="Type">The execution type (market or limit).</param>
/// <param name="Quantity">The number of shares (or contracts) to trade. Must be positive.</param>
/// <param name="LimitPrice">
/// The limit price for <see cref="OrderType.Limit"/> orders.
/// <see langword="null"/> for <see cref="OrderType.Market"/> orders.
/// </param>
public record OrderRequest(
    string Symbol,
    OrderSide Side,
    OrderType Type,
    decimal Quantity,
    decimal? LimitPrice);
