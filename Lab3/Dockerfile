FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TetrisApp/TetrisApp.csproj", "TetrisApp/"]
RUN dotnet restore "TetrisApp/TetrisApp.csproj"

COPY . .
WORKDIR "/src/TetrisApp"
RUN dotnet build "TetrisApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TetrisApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TetrisApp.dll"]