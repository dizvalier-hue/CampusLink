# Use the .NET 10 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["CampusLink.csproj", "./"]
RUN dotnet restore "CampusLink.csproj"

# Copy everything else and publish the app
COPY . .
RUN dotnet publish "CampusLink.csproj" -c Release -o /app/publish

# Use the runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CampusLink.dll"]