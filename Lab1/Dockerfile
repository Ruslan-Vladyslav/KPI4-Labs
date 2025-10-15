FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ConwaysGameOfLife/ConwaysGameOfLife.csproj", "ConwaysGameOfLife/"]
RUN dotnet restore "ConwaysGameOfLife/ConwaysGameOfLife.csproj"

COPY . .
WORKDIR "/src/ConwaysGameOfLife"
RUN dotnet build "ConwaysGameOfLife.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConwaysGameOfLife.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConwaysGameOfLife.dll"]