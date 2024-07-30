FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ScreenSound.API/ScreenSound.API.csproj", "ScreenSound.API/"]
COPY ["ScreenSound.Shared.Data/ScreenSound.Shared.Data.csproj", "ScreenSound.Shared.Data/"]
COPY ["ScreenSound.Shared.Models/ScreenSound.Shared.Models.csproj", "ScreenSound.Shared.Models/"]
RUN dotnet restore "ScreenSound.API/ScreenSound.API.csproj"

COPY . .
WORKDIR "/src/ScreenSound.API"
RUN dotnet build "ScreenSound.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScreenSound.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScreenSound.API.dll"]
