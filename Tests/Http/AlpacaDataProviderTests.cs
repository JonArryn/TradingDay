using System.Net;
using TradingDay.Data.Http;
using Xunit;

namespace TradingDay.Tests.Http;

public sealed class AlpacaDataProviderTests
{
    private static AlpacaDataProvider CreateProvider(HttpMessageHandler handler)
    {
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://example.com") };
        return new AlpacaDataProvider(client);
    }

    [Fact]
    public void GetLatestQuoteAsync_ThrowsNotImplementedException()
    {
        var provider = CreateProvider(new FakeHandler());
        Assert.Throws<NotImplementedException>(() => provider.GetLatestQuoteAsync("AAPL", CancellationToken.None));
    }

    private sealed class FakeHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }
}
