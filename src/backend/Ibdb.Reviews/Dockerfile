#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ibdb.Reviews/Ibdb.Reviews.csproj", "Ibdb.Reviews/"]
RUN dotnet restore "Ibdb.Reviews/Ibdb.Reviews.csproj"
COPY . .
WORKDIR "/src/Ibdb.Reviews"
RUN dotnet build "Ibdb.Reviews.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ibdb.Reviews.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ibdb.Reviews.dll"]