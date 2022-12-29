FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

COPY ./src/services/catalog/ services/catalog/
COPY ./src/building-blocks/ building-blocks/
COPY ./src/shared/ shared/

RUN dotnet restore services/catalog/Host/Host.csproj
RUN dotnet build -c Release --no-restore services/catalog/Host/Host.csproj

WORKDIR /source/services/catalog/Host/

RUN dotnet publish -c Release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build  /app ./

ENTRYPOINT ["dotnet", "FluentPOS.Catalog.Host.dll"]