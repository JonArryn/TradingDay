// See https://aka.ms/new-console-template for more information


using TradingDay.Core.Models;
using TradingDay.Data.Config;
using TradingDay.Data.Http;

var config = AlpacaConfig.FromEnvironment();

var httpClient = new HttpClient();

httpClient.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_KEY_ENV, config.KeyId);
httpClient.DefaultRequestHeaders.Add(AlpacaConfig.ALPACA_API_SECRET_ENV, config.SecretKey);

var provider = new AlpacaDataProvider(httpClient);

var symbol = "AAPL";
var easternZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
var yesterday = DateTimeOffset.UtcNow.ToOffset(easternZone.GetUtcOffset(DateTimeOffset.UtcNow.UtcDateTime)).Date.AddDays(-2);
var from = new DateTimeOffset(yesterday.AddHours(9).AddMinutes(30), easternZone.GetUtcOffset(yesterday));
var to = new DateTimeOffset(yesterday.AddHours(16), easternZone.GetUtcOffset(yesterday));

Console.WriteLine($"Fetching daily bars for {symbol} ({from:yyyy-MM-dd} -> {to:yyyy-MM-dd}...\n"); 

try
{
     var bars = await provider.GetBarsAsync(symbol, from, to);
     
     
     if (bars.Count == 0) {Console.WriteLine("No bars returned.");
         return;
     }

     Console.WriteLine($"{"Date",-12} {"Open",10} {"High",10} {"Low",10} {"Close",10} {"Volume",12}");
     foreach (var bar in bars)
     {
         Console.WriteLine($"{bar.Timestamp:yyyy-MM-dd, -12} {bar.Open, 10:F2} {bar.High, 10:f2} {bar.Low,10:F2} {bar.Close,10:F2} {bar.Volume:12:N0}");
     }

     Console.WriteLine($"\n{bars.Count} bars total.");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message.ToString());
    return; 
}

