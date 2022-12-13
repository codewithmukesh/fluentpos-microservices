FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

COPY ./src/services/identity/src/ services/identity/src/
COPY ./src/building-blocks/ building-blocks/

RUN dotnet restore services/identity/src/Identity.API/Identity.API.csproj
RUN dotnet build -c Release --no-restore services/identity/src/Identity.API/Identity.API.csproj

WORKDIR /source/services/identity/src/Identity.API/

RUN dotnet publish -c Release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build  /app ./

ENTRYPOINT ["dotnet", "FluentPOS.Identity.API.dll"]