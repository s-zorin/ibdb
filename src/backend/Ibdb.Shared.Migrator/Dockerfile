#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ibdb.Shared.Migrator/Ibdb.Shared.Migrator.csproj", "Ibdb.Shared.Migrator/"]
COPY ["Ibdb.Shared/Ibdb.Shared.csproj", "Ibdb.Shared/"]
RUN dotnet restore "Ibdb.Shared.Migrator/Ibdb.Shared.Migrator.csproj"
COPY . .
WORKDIR "/src/Ibdb.Shared.Migrator"
RUN dotnet build "Ibdb.Shared.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ibdb.Shared.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ibdb.Shared.Migrator.dll"]