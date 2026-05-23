using Microsoft.Extensions.Options;
using TradingDay.Core.Interfaces;
using TradingDay.Data.Config;
using TradingDay.Data.Http;
using TradingDay.Runner.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Options
// ---------------------------------------------------------------------------
// Credentials are supplied via environment variables using the ASP.NET Core
// double-underscore convention:  AlpacaConfig__KeyId  /  AlpacaConfig__SecretKey
builder.Services.Configure<AlpacaConfig>(
    builder.Configuration.GetSection(AlpacaConfig.SectionName));

// ---------------------------------------------------------------------------
// HTTP clients
// ---------------------------------------------------------------------------
// Auth headers are set at registration time from IOptions<AlpacaConfig>;
// they are never read inline at the call site.
builder.Services
    .AddHttpClient<IMarketDataProvider, AlpacaDataProvider>((sp, client) =>
    {
        var cfg = sp.GetRequiredService<IOptions<AlpacaConfig>>().Value;
        client.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_KEY_ENV, cfg.KeyId);
        client.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_SECRET_ENV, cfg.SecretKey);
    });

// ---------------------------------------------------------------------------
// Controllers & hosted services
// ---------------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddHostedService<MarketDataSmokeTestService>();

// ---------------------------------------------------------------------------
// Build & run
// ---------------------------------------------------------------------------
var app = builder.Build();

app.MapControllers();

app.Run();
