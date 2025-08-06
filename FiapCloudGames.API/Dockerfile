# Compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Restore as distinct layers
RUN dotnet restore "./FiapCloudGames/FiapCloudGames.API.csproj"
RUN dotnet publish "./FiapCloudGames/FiapCloudGames.API/.csproj" \
    -c Release -o out
COPY . .

# Build com a runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /App
COPY --from=build-env /App/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "FiapCloudGames.API.dll"]
