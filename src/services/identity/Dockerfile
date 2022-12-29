FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

COPY ./src/services/identity/ services/identity/
COPY ./src/building-blocks/ building-blocks/

RUN dotnet restore services/identity/Host/Host.csproj
RUN dotnet build -c Release --no-restore services/identity/Host/Host.csproj

WORKDIR /source/services/identity/Host/

RUN dotnet publish -c Release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build  /app ./

ENTRYPOINT ["dotnet", "FluentPOS.Identity.Host.dll"]