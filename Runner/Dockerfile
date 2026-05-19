FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TradingDay.Runner/TradingDay.Runner.csproj", "TradingDay.Runner/"]
COPY ["TradingDay.Core/TradingDay.Core.csproj", "TradingDay.Core"]
COPY ["TradingDay.Core/TradingDay.Core.csproj", "TradingDay.Core"]
RUN dotnet restore "TradingDay.Runner/TradingDay.Runner.csproj"
COPY . .
WORKDIR "/src/TradingDay.Runner"
RUN dotnet build "./TradingDay.Runner.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TradingDay.Runner.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradingDay.Runner.dll"]
