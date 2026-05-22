# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build TradingDay.sln

# Run (requires env vars — see Configuration below)
dotnet run --project Runner

# Run via Docker (uses Runner/.env for credentials)
docker compose up
```

## Architecture

Three-project .NET 10 solution with a strict dependency direction:

```
Runner → Data → Core
```

- **Core** — domain types only: `Bar` (OHLCV record) and `IMarketDataProvider`. Depends on MathNet.Numerics (not yet used; reserved for indicator math).
- **Data** — implements `IMarketDataProvider` via `AlpacaDataProvider`, which calls the Alpaca Market Data REST API (`data.alpaca.markets/v2`). JSON deserialization uses `file`-scoped private DTOs inside `AlpacaDataProvider.cs`. Config is loaded from environment variables via `AlpacaConfig.FromEnvironment()`.
- **Runner** — console entry point. Wires dependencies manually (no DI container): creates `HttpClient`, attaches Alpaca auth headers, instantiates `AlpacaDataProvider`, and prints bars to stdout.

## Configuration

Alpaca credentials are read from two environment variables:

| Variable | Header sent to Alpaca |
|---|---|
| `APCA-API-KEY-ID` | `APCA-API-KEY-ID` |
| `APCA-API-SECRET-KEY` | `APCA-API-SECRET-KEY` |

For local development, set these in `Runner/.env` (loaded by Docker Compose) or `Runner/Properties/launchSettings.json` (loaded by Rider/VS). Both files are gitignored.

## Automated Workflow (Linear → Claude Code)

Tickets in the **TRD** Linear team flow through automated implementation overnight:

1. A ticket is moved to **"Ready for Claude"** status in Linear
2. A webhook fires to a Vercel relay, which triggers the `claude-linear.yml` GitHub Action
3. Claude Code checks out this repo, creates a branch named exactly after the Linear ticket ID (e.g. `TRD-42`), implements the ticket, and opens a PR
4. The PR title includes the Linear ticket ID so Linear auto-links and moves the ticket to In Review

**When implementing a ticket:**
- Read the existing codebase before writing anything — understand the current structure first
- Respect the `Runner → Data → Core` dependency direction strictly; do not introduce cycles
- Establish and use a DI Container — wire dependencies through interfaces always
- **Every PR must include tests.** Add or extend a test project as needed. Tests must pass before committing
- Branch name must match the Linear ticket ID exactly (e.g. `TRD-42`)
- Commit message format: `feat: TRD-{n} {ticket title}`
- Do not modify `Runner/.env`, `launchSettings.json`, or any gitignored credential files