using System.Net;
using Microsoft.Extensions.Options;
using TradingDay.Core.Models;
using TradingDay.Data.Config;
using TradingDay.Data.Http;
using Xunit;

namespace TradingDay.Tests.Http;

public sealed class AlpacaBrokerTests
{
    private static AlpacaBroker CreateBroker()
    {
        var client = new HttpClient(new FakeHandler()) { BaseAddress = new Uri("https://example.com") };
        var options = Options.Create(new AlpacaConfig { KeyId = "key", SecretKey = "secret" });
        return new AlpacaBroker(client, options);
    }

    [Fact]
    public void PlaceOrderAsync_ThrowsNotImplementedException()
    {
        var broker = CreateBroker();
        var request = new OrderRequest("AAPL", OrderSide.Buy, OrderType.Market, 1m, null);
        Assert.Throws<NotImplementedException>(() => broker.PlaceOrderAsync(request, CancellationToken.None));
    }

    [Fact]
    public void CancelOrderAsync_ThrowsNotImplementedException()
    {
        var broker = CreateBroker();
        Assert.Throws<NotImplementedException>(() => broker.CancelOrderAsync("order-1", CancellationToken.None));
    }

    [Fact]
    public void GetPositionsAsync_ThrowsNotImplementedException()
    {
        var broker = CreateBroker();
        Assert.Throws<NotImplementedException>(() => broker.GetPositionsAsync(CancellationToken.None));
    }

    [Fact]
    public void GetAccountInfoAsync_ThrowsNotImplementedException()
    {
        var broker = CreateBroker();
        Assert.Throws<NotImplementedException>(() => broker.GetAccountInfoAsync(CancellationToken.None));
    }

    [Fact]
    public void GetOrderAsync_ThrowsNotImplementedException()
    {
        var broker = CreateBroker();
        Assert.Throws<NotImplementedException>(() => broker.GetOrderAsync("order-1", CancellationToken.None));
    }

    private sealed class FakeHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }
}
