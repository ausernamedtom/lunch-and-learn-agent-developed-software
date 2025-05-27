# Use the .NET SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["API.csproj", "./"]
RUN dotnet restore "API.csproj"

# Copy all files
COPY . .

# Build the application
RUN dotnet build "API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose ports
EXPOSE 80
EXPOSE 443

# Set entry point
ENTRYPOINT ["dotnet", "API.dll"]
