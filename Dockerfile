FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY IDNT.API/ .
COPY IDNT.TravelRoutes/ ./IDNT.TravelRoutes/

COPY IDNT.TravelRoutes/IDNT.TravelRoutes.csproj ./IDNT.TravelRoutes/
COPY IDNT.API/IDNT.API.csproj .

RUN dotnet restore "./IDNT.API.csproj"
RUN dotnet build "./IDNT.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IDNT.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IDNT.API.dll"]


