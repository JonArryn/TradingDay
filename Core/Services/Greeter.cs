using TradingDay.Core.Interfaces;

namespace TradingDay.Core.Services;

public sealed class Greeter : IGreeter
{
    public const string Message = "Hello, TradingDay!";

    public string Greet() => Message;
}
