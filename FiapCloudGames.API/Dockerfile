FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
COPY . .
RUN dotnet restore "./FiapCloudGames.API/FiapCloudGames.API.csproj"
RUN dotnet publish "./FiapCloudGames.API/FiapCloudGames.API.csproj" \
    -c Release -o /publish --no-self-contained --no-restore
COPY ./appsettings.json /publish/appsettings.json

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /App
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","FiapCloudGames.API.dll"]
