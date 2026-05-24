namespace TradingDay.Core.Models;

/// <summary>A snapshot of the trading account's financial state.</summary>
/// <param name="AccountId">The broker-assigned unique identifier for the account.</param>
/// <param name="BuyingPower">
/// The amount of capital available to open new positions,
/// taking into account leverage and unsettled funds.
/// </param>
/// <param name="Cash">The settled cash balance in the account.</param>
/// <param name="PortfolioValue">The total value of all positions plus cash.</param>
/// <param name="Currency">The ISO 4217 currency code for all monetary values (e.g. <c>"USD"</c>).</param>
public record AccountInfo(
    string AccountId,
    decimal BuyingPower,
    decimal Cash,
    decimal PortfolioValue,
    string Currency);
