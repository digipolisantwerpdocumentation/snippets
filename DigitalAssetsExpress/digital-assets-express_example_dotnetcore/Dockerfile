FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app
 
# Copy csproj and restore
COPY DigitalAssetsExpressExample.csproj .
RUN dotnet restore

# Copy everything else
COPY . .

# Publish
RUN dotnet publish -c Release -o out --no-restore DigitalAssetsExpressExample.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim
WORKDIR /app

COPY --from=build-env /app/out/ .

EXPOSE 80

CMD ["dotnet", "DigitalAssetsExpressExample.dll"]
