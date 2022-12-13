FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

COPY ./src/api-gateway api-gateway/
COPY ./src/building-blocks/ building-blocks/

RUN dotnet restore ./api-gateway/Gateway.csproj
RUN dotnet build -c Release --no-restore ./api-gateway/Gateway.csproj

WORKDIR /source/api-gateway/

RUN dotnet publish -c Release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build  /app ./

ENTRYPOINT ["dotnet", "FluentPOS.Gateway.dll"]