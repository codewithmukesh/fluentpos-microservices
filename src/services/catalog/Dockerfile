FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

COPY ./services/catalog/src/ ./services/catalog/src/
COPY ./building-blocks/ ./building-blocks/

RUN dotnet restore ./services/catalog/src/Catalog.API/Catalog.API.csproj
RUN dotnet build -c Release --no-restore ./services/catalog/src/Catalog.API/Catalog.API.csproj

WORKDIR /source/services/catalog/src/Catalog.API/

RUN dotnet publish -c Release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build  /app ./

ENTRYPOINT ["dotnet", "FluentPOS.Catalog.API.dll"]