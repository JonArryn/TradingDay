using TradingDay.Core.Interfaces;
using TradingDay.Core.Services;
using Xunit;

namespace TradingDay.Core.Tests;

public class GreeterTests
{
    [Fact]
    public void Greet_ReturnsHelloTradingDayMessage()
    {
        IGreeter greeter = new Greeter();

        var message = greeter.Greet();

        Assert.Equal("Hello, TradingDay!", message);
    }

    [Fact]
    public void Message_Constant_IsHelloTradingDay()
    {
        Assert.Equal("Hello, TradingDay!", Greeter.Message);
    }
}
