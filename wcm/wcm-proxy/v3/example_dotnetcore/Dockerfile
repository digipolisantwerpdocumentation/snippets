FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
 
# Copy csproj and restore
COPY WcmProxyExample.csproj .
RUN dotnet restore

# Copy everything else
COPY . .

# Publish
RUN dotnet publish -c Release -o out --no-restore WcmProxyExample.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build-env /app/out/ .

EXPOSE 80

CMD ["dotnet", "WcmProxyExample.dll"]
