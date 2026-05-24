namespace TradingDay.Core.Models;

/// <summary>Represents the lifecycle state of an order.</summary>
public enum OrderStatus
{
    /// <summary>The order has been submitted and is awaiting processing.</summary>
    Pending,

    /// <summary>A portion of the order quantity has been filled.</summary>
    PartiallyFilled,

    /// <summary>The entire order quantity has been filled.</summary>
    Filled,

    /// <summary>The order was cancelled before it was fully filled.</summary>
    Cancelled,

    /// <summary>The order was rejected by the broker.</summary>
    Rejected,
}
