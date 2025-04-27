# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./ConfigApi.csproj"
RUN dotnet publish "./ConfigApi.csproj" -c Release -o /app/publish

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ConfigApi.dll"]
