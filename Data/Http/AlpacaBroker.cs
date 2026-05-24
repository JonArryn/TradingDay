using Microsoft.Extensions.Options;
using TradingDay.Core.Interfaces;
using TradingDay.Core.Models;
using TradingDay.Data.Config;

namespace TradingDay.Data.Http;

/// <summary>
/// Alpaca Markets implementation of <see cref="IBroker"/>.
/// All methods are currently stubs and will throw <see cref="NotImplementedException"/>
/// until the corresponding Alpaca Trading API calls are wired up.
/// </summary>
public sealed class AlpacaBroker : IBroker
{
    private readonly HttpClient _http;
    private readonly AlpacaConfig _config;

    /// <summary>
    /// Initialises a new <see cref="AlpacaBroker"/> with the supplied HTTP client and configuration.
    /// </summary>
    /// <param name="http">The typed <see cref="HttpClient"/> pre-configured with Alpaca auth headers.</param>
    /// <param name="options">Resolved <see cref="AlpacaConfig"/> options.</param>
    public AlpacaBroker(HttpClient http, IOptions<AlpacaConfig> options)
    {
        _http = http;
        _config = options.Value;
    }

    /// <inheritdoc/>
    public Task<OrderResult> PlaceOrderAsync(OrderRequest request, CancellationToken ct) =>
        throw new NotImplementedException("PlaceOrderAsync is pending Alpaca Trading API implementation.");

    /// <inheritdoc/>
    public Task<bool> CancelOrderAsync(string orderId, CancellationToken ct) =>
        throw new NotImplementedException("CancelOrderAsync is pending Alpaca Trading API implementation.");

    /// <inheritdoc/>
    public Task<IReadOnlyList<Position>> GetPositionsAsync(CancellationToken ct) =>
        throw new NotImplementedException("GetPositionsAsync is pending Alpaca Trading API implementation.");

    /// <inheritdoc/>
    public Task<AccountInfo> GetAccountInfoAsync(CancellationToken ct) =>
        throw new NotImplementedException("GetAccountInfoAsync is pending Alpaca Trading API implementation.");

    /// <inheritdoc/>
    public Task<OrderResult> GetOrderAsync(string orderId, CancellationToken ct) =>
        throw new NotImplementedException("GetOrderAsync is pending Alpaca Trading API implementation.");
}
