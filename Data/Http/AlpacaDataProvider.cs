using System.Text.Json;
using System.Text.Json.Serialization;
using TradingDay.Core.Interfaces;
using TradingDay.Core.Models;

namespace TradingDay.Data.Http;

/// <summary>Alpaca Markets implementation of <see cref="IMarketDataProvider"/>.</summary>
public sealed class AlpacaDataProvider(HttpClient http) : IMarketDataProvider
{
    private const string BaseUrl = "https://data.alpaca.markets";
    private static readonly JsonSerializerOptions JsonOpts = new() {PropertyNameCaseInsensitive = true};

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Bar>> GetBarsAsync(string symbol, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default)
    {
        var start = Uri.EscapeDataString(from.UtcDateTime.ToString("o"));
        var end = Uri.EscapeDataString(to.UtcDateTime.ToString("o"));
        var url = $"{BaseUrl}/v2/stocks/{symbol}/bars?timeframe=1Day&start={start}&end={end}&limit=1000";
        // var url = $"{BaseUrl}/v2/stocks/{symbol}/bars?limit=1000&adjustment=raw&feed=sip&sort=asc";

        var response = await http.GetAsync(url, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(
                $"Alpaca API error {(int)response.StatusCode} {response.ReasonPhrase}: {errorBody}");
        }
        
        var json = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        var envelope = JsonSerializer.Deserialize<AlpacaBarsEnvelope>(json, JsonOpts) 
                       ?? throw new InvalidOperationException("Alpaca returned null.");

        return envelope.Bars
            .Select(b => new Bar(symbol, b.Timestamp, b.Open, b.High, b.Low, b.Close, b.Volume))
            .ToList()
            .AsReadOnly();
    }

    /// <inheritdoc/>
    public Task<Quote> GetLatestQuoteAsync(string symbol, CancellationToken ct) =>
        throw new NotImplementedException("GetLatestQuoteAsync is pending Alpaca Market Data API implementation.");
}

file sealed class AlpacaBarsEnvelope
{
    [JsonPropertyName("bars")] public List<AlpacaBar> Bars { get; init; } = [];
}

file sealed class AlpacaBar
{
    [JsonPropertyName("t")] public DateTimeOffset Timestamp { get; init; }
    [JsonPropertyName("o")] public decimal Open { get; init; }
    [JsonPropertyName("h")] public decimal High { get; init; }
    [JsonPropertyName("l")] public decimal Low { get; init; }
    [JsonPropertyName("c")] public decimal Close { get; init; }
    [JsonPropertyName("v")] public long Volume { get; init; }
}