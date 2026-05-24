namespace TradingDay.Core.Models;

/// <summary>Specifies the execution type of an order.</summary>
public enum OrderType
{
    /// <summary>Execute at the best available market price.</summary>
    Market,

    /// <summary>Execute only at the specified limit price or better.</summary>
    Limit,
}
