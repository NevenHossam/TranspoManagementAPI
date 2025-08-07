# Use official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy the csproj file and restore dependencies
COPY ["TranspoManagementAPI/TranspoManagementAPI.csproj", "TranspoManagementAPI/"]
RUN dotnet restore "TranspoManagementAPI/TranspoManagementAPI.csproj"
# Copy the full project and build it
COPY . .
WORKDIR /src/TranspoManagementAPI
RUN dotnet publish "TranspoManagementAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false
# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "TranspoManagementAPI.dll"]
