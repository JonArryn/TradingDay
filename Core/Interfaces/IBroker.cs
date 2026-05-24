using TradingDay.Core.Models;

namespace TradingDay.Core.Interfaces;

/// <summary>
/// Provider-agnostic abstraction for order execution and account management.
/// Concrete implementations (e.g. Alpaca, IBKR) live in <c>TradingDay.Data</c>.
/// </summary>
public interface IBroker
{
    /// <summary>Submits an order to the broker and returns the initial order state.</summary>
    /// <param name="request">The order details including symbol, side, type, and quantity.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An <see cref="OrderResult"/> reflecting the submitted order's initial state.</returns>
    Task<OrderResult> PlaceOrderAsync(OrderRequest request, CancellationToken ct);

    /// <summary>Requests cancellation of an open order.</summary>
    /// <param name="orderId">The broker-assigned order identifier.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns><see langword="true"/> if the cancellation was accepted; <see langword="false"/> otherwise.</returns>
    Task<bool> CancelOrderAsync(string orderId, CancellationToken ct);

    /// <summary>Returns all currently open positions held in the account.</summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A read-only list of <see cref="Position"/> records.</returns>
    Task<IReadOnlyList<Position>> GetPositionsAsync(CancellationToken ct);

    /// <summary>Returns a snapshot of the account's current financial state.</summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An <see cref="AccountInfo"/> record with current balances and buying power.</returns>
    Task<AccountInfo> GetAccountInfoAsync(CancellationToken ct);

    /// <summary>Retrieves the latest state of a specific order.</summary>
    /// <param name="orderId">The broker-assigned order identifier.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An <see cref="OrderResult"/> reflecting the order's current state.</returns>
    Task<OrderResult> GetOrderAsync(string orderId, CancellationToken ct);
}
