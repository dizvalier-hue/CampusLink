# Use the .NET 10 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and publish the app
COPY . ./
RUN dotnet publish -c Release -o out

# Use the .NET 10 runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out ./

# This tells Railway how to start your app
ENTRYPOINT ["dotnet", "CampusLink.dll"]